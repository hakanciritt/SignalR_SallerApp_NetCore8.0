using SignalR_App.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalR_App.Domain.Entitites
{
    [Table("Sliders")]
    public class Slider : BaseFullEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ButtonText { get; set; }
        public string ButtonAction { get; set; }
        public Status Status { get; set; }
    }
}
