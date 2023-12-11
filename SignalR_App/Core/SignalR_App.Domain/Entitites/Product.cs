using SignalR_App.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalR_App.Domain.Entitites
{
    [Table("Products")]
    public class Product : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public Status Status { get; set; }
        public int? MetaId { get; set; }
        public Meta Meta { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
