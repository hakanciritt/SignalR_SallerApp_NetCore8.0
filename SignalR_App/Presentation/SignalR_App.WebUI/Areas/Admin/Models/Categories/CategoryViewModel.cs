using SignalR_App.Domain.Enums;

namespace SignalR_App.WebUI.Areas.Admin.Models.Categories
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModificationDate { get; set; }

        public string? Name { get; set; }
        public Status Status { get; set; }
        public int? MetaId { get; set; }
        public MetaViewModel Meta { get; set; }

        //public ICollection<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
