using System.ComponentModel.DataAnnotations.Schema;

namespace SignalR_App.Domain.Entitites
{
    [Table("Permissions")]
    public class AppPermission : BaseFullEntity
    {
        public string Name { get; set; }
    }
}
