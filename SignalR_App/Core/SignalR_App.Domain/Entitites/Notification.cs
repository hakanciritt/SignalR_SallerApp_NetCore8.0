using System.ComponentModel.DataAnnotations.Schema;

namespace SignalR_App.Domain.Entitites
{
    [Table("Notifications")]
    public class Notification : BaseEntity
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }

    }
}
