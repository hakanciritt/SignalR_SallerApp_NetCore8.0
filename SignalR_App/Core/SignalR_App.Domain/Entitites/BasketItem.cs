using System.ComponentModel.DataAnnotations.Schema;

namespace SignalR_App.Domain.Entitites
{
    [Table("BasketItems")]
    public class BasketItem : BaseFullEntity
    {

        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Basket Basket { get; set; }
        public int BasketId { get; set; }

    }
}
