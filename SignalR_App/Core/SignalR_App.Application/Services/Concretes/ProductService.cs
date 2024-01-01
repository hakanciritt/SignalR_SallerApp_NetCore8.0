using Microsoft.EntityFrameworkCore;
using SignalR_App.Application.Dtos.ProductDtos;
using SignalR_App.Application.Mappings;
using SignalR_App.Application.Redis;
using SignalR_App.Application.Repositories;
using SignalR_App.Application.Services.Abstracts;
using SignalR_App.Domain.Entitites;
using SignalR_App.Domain.Result;
using SignalR_App.Domain.Results;
using System.Text.Json;

namespace SignalR_App.Application.Services.Concretes
{
    public class ProductService(IRepository<Product, int> productRepository,
        RedisConfiguration redis) : IProductService
    {
        private readonly IRepository<Product, int> _productRepository = productRepository;
        private readonly RedisConfiguration _redis = redis;

        public async Task<List<ProductDto>> GetAll()
        {
            var result = await _productRepository.GetAll()
                .Include(c => c.Category)
                .Where(c => c.Status == Domain.Enums.Status.Active).ToListAsync();
            return ObjectMapper.Map.Map<List<ProductDto>>(result);
        }
        public async Task<DataResult<ProductDto>> GetById(int id)
        {
            var result = await _productRepository.GetAll()
                .Include(c => c.Meta)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (result is null) return DataResult<ProductDto>.Failed("Ürün bulunamadı");

            return DataResult<ProductDto>.Successed(ObjectMapper.Map.Map<ProductDto>(result));
        }
        public async Task<Result> Create(ProductDto product)
        {
            var mapping = ObjectMapper.Map.Map<Product>(product);
            var result = await _productRepository.InsertAsync(mapping);
            await _productRepository.SaveChangesAsync();
            var connection = redis.Connect();

            await connection.StringSetAsync($"product-{result.Id}:category-{result.Category.Id}", JsonSerializer.Serialize(result));

            return result != null ? Result.Successed("Ürün başarıyla kaydedildi.") : Result.Failed();
        }
        public async Task<Result> Delete(int id)
        {
            await _productRepository.DeleteAsync(id);
            await _productRepository.SaveChangesAsync();
            var connection = redis.Connect();
            await connection.KeyDeleteAsync($"product-{id}:*");
            return Result.Successed("Başarıyla silindi");
        }
        public async Task<Result> Update(ProductDto product)
        {
            var result = await _productRepository.GetAll()
                .Include(c => c.Category)
                .Include(c => c.Meta)
                .FirstOrDefaultAsync(c => c.Id == product.Id);

            ObjectMapper.Map.Map(product, result);
            await _productRepository.SaveChangesAsync();

            var connection = redis.Connect();

            if (await connection.KeyExistsAsync($"product-{result?.Id}:*"))
            {
                await connection.KeyDeleteAsync($"product-{result?.Id}:*");
            }

            await connection.StringSetAsync($"product-{result?.Id}:category-{result?.Category?.Id}", JsonSerializer.Serialize(result));

            return Result.Successed("Başarıyla güncellendi");
        }
    }
}
