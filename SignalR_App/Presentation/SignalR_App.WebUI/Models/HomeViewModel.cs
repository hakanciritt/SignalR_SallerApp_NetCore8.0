using SignalR_App.Application.Dtos.Dtos;
using SignalR_App.Application.Dtos.ProductDtos;

namespace SignalR_App.WebUI.Models
{
    public class HomeViewModel
    {
        public List<SliderDto> Sliders { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
