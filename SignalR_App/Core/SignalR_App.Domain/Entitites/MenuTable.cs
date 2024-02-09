using SignalR_App.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalR_App.Domain.Entitites
{
    [Table("MenuTables")]
    public class MenuTable : BaseFullEntity
    {
        public string Name { get; set; }
        public Status Status { get; set; }

        public int? BasketId { get; set; }
        public Basket Basket { get; set; }
    }
}
