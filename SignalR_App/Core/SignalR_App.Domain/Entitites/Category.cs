using SignalR_App.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalR_App.Domain.Entitites
{
    [Table("Categories")]
    public class Category : BaseEntity
    {
        public string? Name { get; set; }
        public Status Status { get; set; }
        public int? MetaId { get; set; }
        public Meta Meta { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
