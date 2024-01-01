using SignalR_App.Application.Dtos.ProductDtos;

namespace SignalR_App.Application.Dtos.BasketDtos
{
    public class BasketItemDto : BaseFullEntityDto
    {
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public BasketDto Basket { get; set; }
        public int BasketId { get; set; }
    }
}
