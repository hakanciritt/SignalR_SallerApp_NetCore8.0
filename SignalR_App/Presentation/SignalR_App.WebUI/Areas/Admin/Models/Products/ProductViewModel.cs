using SignalR_App.Domain.Enums;
using SignalR_App.WebUI.Areas.Admin.Models.Categories;

namespace SignalR_App.WebUI.Areas.Admin.Models.Products
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public Status Status { get; set; }
        public int? MetaId { get; set; }
        public MetaViewModel Meta { get; set; } = new();

        public int? CategoryId { get; set; }
        public CategoryViewModel Category { get; set; } = new();
    }
}
