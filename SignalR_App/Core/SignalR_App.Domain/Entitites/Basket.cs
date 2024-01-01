using SignalR_App.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalR_App.Domain.Entitites
{
    [Table("Baskets")]
    public class Basket : BaseFullEntity
    {
        public decimal TotalPrice { get; set; }
        public int MenuTableId { get; set; }

        public Status Status { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
    }
}
