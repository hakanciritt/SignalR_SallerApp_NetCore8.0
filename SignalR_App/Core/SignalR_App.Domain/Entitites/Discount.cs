using SignalR_App.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalR_App.Domain.Entitites
{
    [Table("Discounts")]
    public class Discount : BaseEntity
    {
        public string? Title { get; set; }
        public decimal Value { get; set; }
        public string? Description { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public DiscountType DiscountType { get; set; }
        public DiscountScope DiscountScope { get; set; }

    }
}
