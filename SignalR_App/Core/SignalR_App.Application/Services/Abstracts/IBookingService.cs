using SignalR_App.Domain.Entitites;

namespace SignalR_App.Application.Services.Abstracts
{
    public interface IBookingService
    {
        Task<List<Booking>> GetAll();
    }
}
