using SignalR_App.Application.Dtos.Dtos;

namespace SignalR_App.Application.WebServices
{
    public interface IWebService
    {
        Task<List<SliderDto>> GetAllSliders();
    }
}
