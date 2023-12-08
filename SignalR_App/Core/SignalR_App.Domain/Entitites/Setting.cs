using System.ComponentModel.DataAnnotations.Schema;

namespace SignalR_App.Domain.Entitites
{
    [Table("Settings")]
    public class Setting : BaseEntity
    {
        public string? Key { get; set; }
        public string? Value { get; set; }

    }
}
