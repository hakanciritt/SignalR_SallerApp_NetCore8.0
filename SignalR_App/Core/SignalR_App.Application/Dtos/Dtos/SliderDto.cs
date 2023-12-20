using SignalR_App.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR_App.Application.Dtos.Dtos
{
    public class SliderDto : BaseFullEntityDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ButtonText { get; set; }
        public string ButtonAction { get; set; }
        public Status Status { get; set; }
    }
}
