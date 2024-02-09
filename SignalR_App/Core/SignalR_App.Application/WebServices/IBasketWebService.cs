using SignalR_App.Application.Dtos.BasketDtos;

namespace SignalR_App.Application.WebServices
{
    public interface IBasketWebService
    {
        Task<BasketDto> GetBasketByTableNumber(int tableNumber);
    }
}
