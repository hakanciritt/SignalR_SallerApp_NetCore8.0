using System.ComponentModel.DataAnnotations.Schema;

namespace SignalR_App.Domain.Entitites
{
    [Table("Contacts")]
    public class Contact : BaseEntity
    {
        public string NameSurname { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
    }
}
