using SignalR_App.Application.Dtos.Dtos;
using SignalR_App.Application.Dtos.ProductDtos;

namespace SignalR_App.Application.WebServices
{
    public interface IWebService
    {
        Task<List<SliderDto>> GetAllSliders();
        Task<List<ProductDto>> GetAllProducts();
    }
}
