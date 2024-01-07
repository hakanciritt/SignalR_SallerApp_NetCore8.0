using SignalR_App.Application.Dtos.BookingDto;
using SignalR_App.Application.Dtos.ContactDtos;
using SignalR_App.Domain.Result;
using SignalR_App.Domain.Results;

namespace SignalR_App.Application.Services.Abstracts
{
    public interface IContactService
    {
        Task<Result> Create(ContactDto contact);
        Task<DataResult<ContactDto>> GetById(int id);
        Task<List<ContactDto>> GetAll();
        Task<Result> Delete(int id);

    }
}
