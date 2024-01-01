using Microsoft.EntityFrameworkCore;
using SignalR_App.Application.Dtos.Dtos;
using SignalR_App.Application.Dtos.ProductDtos;
using SignalR_App.Application.Mappings;
using SignalR_App.Application.Redis;
using SignalR_App.Application.Repositories;
using SignalR_App.Domain.Entitites;
using StackExchange.Redis;
using System.Text.Json;

namespace SignalR_App.Application.WebServices
{
    public class WebService(IRepository<Slider, int> sliderRepository,
        RedisConfiguration redis, IRepository<Product, int> productRepository) : IWebService
    {
        private readonly IRepository<Slider, int> _sliderRepository = sliderRepository;
        private readonly RedisConfiguration _redis = redis;
        private readonly IRepository<Product, int> _productRepository = productRepository;

        public async Task<List<SliderDto>> GetAllSliders()
        {
            var connection = _redis.Connect();
            var check = await connection.KeyExistsAsync("Sliders");

            if (check)
            {
                var result = await connection.StringGetAsync("Sliders");
                return JsonSerializer.Deserialize<List<SliderDto>>(result);
            }

            var sliders = await _sliderRepository.GetAll().Where(c => c.Status == Domain.Enums.Status.Active).ToListAsync();
            var map = ObjectMapper.Map.Map<List<SliderDto>>(sliders);
            await connection.StringSetAsync("Sliders", JsonSerializer.Serialize(map));
            return map;
        }

        public async Task<List<ProductDto>> GetAllProducts()
        {
            var servers = _redis.ConnectionMultiplexer.GetEndPoints();
            var connection = _redis.Connect();
            var keys = new List<RedisKey>();

            foreach (var item in servers)
            {
                var server = _redis.ConnectionMultiplexer.GetServer(item);
                keys.AddRange(server.Keys(pattern: $"product-*:category-*"));
            }

            var productsCount = await _productRepository.GetAll()
                .CountAsync(c => c.Status == Domain.Enums.Status.Active && c.Category.Status == Domain.Enums.Status.Active);

            if (productsCount != keys.Count)
            {
                var products = await _productRepository.GetAll()
                    .Include(c => c.Category)
                    .Include(c => c.Meta)
                    .Where(c => c.Status == Domain.Enums.Status.Active && c.Category.Status == Domain.Enums.Status.Active).ToListAsync();

                var mapping = ObjectMapper.Map.Map<List<ProductDto>>(products);
                mapping.ForEach(c => c.Category.Products = null);

                await connection.StringSetAsync(
                    mapping.Select(d => new KeyValuePair<RedisKey, RedisValue>($"product-{d.Id}:category-{d.Category?.Id}",
                    JsonSerializer.Serialize(d))).ToArray());

                return mapping;
            }

            var stringProducts = await connection.StringGetAsync(keys.ToArray());
            var deserialize = stringProducts.Select(c => Convert.ToString(c)).Select(d => JsonSerializer.Deserialize<ProductDto>(d)).ToList();
            return deserialize;
        }
    }
}
