using SignalR_App.Application.Dtos.ProductDtos;
using SignalR_App.Domain.Enums;

namespace SignalR_App.Application.Dtos.CategoryDto
{
    public class CategoryDto : BaseEntityDto
    {
        public string? Name { get; set; }
        public Status Status { get; set; }
        public int? MetaId { get; set; }
        public MetaDto.MetaDto Meta { get; set; } = new();

        public ICollection<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
