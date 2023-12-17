using SignalR_App.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalR_App.Domain.Entitites
{
    [Table("Orders")]
    public class Order : BaseEntity<Guid>
    {

        public int TableNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public Status Status { get; set; }


        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
