using SignalR_App.Domain.Enums;

namespace SignalR_App.Application.Dtos.ProductDtos
{
    public class ProductDto : BaseEntityDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public Status Status { get; set; }
        public int? MetaId { get; set; }
        public MetaDto.MetaDto Meta { get; set; } = new();

        public int? CategoryId { get; set; }
        public CategoryDto.CategoryDto? Category { get; set; }
    }
}
