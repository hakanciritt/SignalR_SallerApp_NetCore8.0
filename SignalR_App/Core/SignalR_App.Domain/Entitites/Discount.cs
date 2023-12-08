using System.ComponentModel.DataAnnotations.Schema;

namespace SignalR_App.Domain.Entitites
{
    [Table("Discounts")]
    public class Discount : BaseEntity
    {
        public string? Title { get; set; }
        public  decimal Amount { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}
