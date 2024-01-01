using Microsoft.EntityFrameworkCore;
using SignalR_App.Application.Dtos.BasketDtos;
using SignalR_App.Application.Mappings;
using SignalR_App.Application.Repositories;
using SignalR_App.Domain.Entitites;
using SignalR_App.Domain.Results;

namespace SignalR_App.Application.WebServices
{
    public class BasketWebService : IBasketWebService
    {
        private readonly IRepository<Basket, int> _basketRepository;
        private readonly IRepository<Product, int> _productRepository;

        public BasketWebService(IRepository<Basket, int> basketRepository,
            IRepository<Product, int> productRepository)
        {
            _basketRepository = basketRepository;
            _productRepository = productRepository;
        }
        public async Task<BasketDto> GetBasketByTableNumber(int tableNumber)
        {
            var basket = await _basketRepository.GetAll()
                .Include(c => c.BasketItems)
                    .ThenInclude(c => c.Product)
                .FirstOrDefaultAsync(c => c.MenuTableId == tableNumber);

            return ObjectMapper.Map.Map<BasketDto>(basket);
        }
        public async Task<DataResult<BasketDto>> Create(CreateBasketDto basket)
        {
            var checkBasket = await _basketRepository.GetAll().AnyAsync(c => c.MenuTableId == basket.MenuTableId);

            var product = await _productRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == basket.ProductId);

            if (product is null) return DataResult<BasketDto>.Failed("Ürün bulunamadı");

            if (!checkBasket)
            {
                var basketModel = new Basket()
                {
                    MenuTableId = basket.MenuTableId,
                    Status = Domain.Enums.Status.Active,
                    BasketItems = new List<BasketItem>(),
                };

                basketModel.BasketItems.Add(new BasketItem()
                {
                    ProductId = basket.ProductId,
                    Price = product.Price,
                    IsDeleted = false,
                    Quantity = basket.Quantity,
                });

                basketModel.TotalPrice = basketModel.BasketItems.Sum(c => c.Price * c.Quantity);

                var basketResult = await _basketRepository.InsertAsync(basketModel);

                await _basketRepository.SaveChangesAsync();
                return DataResult<BasketDto>.Successed(ObjectMapper.Map.Map<BasketDto>(basketResult));
            }

            var basketRes = await _basketRepository.GetAll().Include(c => c.BasketItems)
                    .FirstOrDefaultAsync(c => c.MenuTableId == basket.MenuTableId);

            if (basketRes.BasketItems.FirstOrDefault(c => c.ProductId == basket.ProductId) != null)
            {
                basketRes.BasketItems.FirstOrDefault(c => c.ProductId == basket.ProductId).Quantity++;
            }
            else
            {
                basketRes?.BasketItems.Add(new BasketItem()
                {
                    IsDeleted = false,
                    Price = product.Price,
                    Quantity = basket.Quantity
                });
            }
            await _basketRepository.SaveChangesAsync();
            return DataResult<BasketDto>.Successed(ObjectMapper.Map.Map<BasketDto>(basketRes));
        }
    }
}
