using SignalR_App.Application.Dtos.BookingDto;
using SignalR_App.Domain.Result;
using SignalR_App.Domain.Results;

namespace SignalR_App.Application.Services.Abstracts
{
    public interface IBookingService
    {
        Task<List<BookingDto>> GetAll();
        Task<Result> Create(BookingDto booking);
        Task<Result> Delete(int id);
        Task<Result> Update(BookingDto booking);
        Task<DataResult<BookingDto>> GetById(int id);
    }
}
