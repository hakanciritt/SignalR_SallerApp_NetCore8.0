using AutoMapper;
using SignalR_App.Application.Dtos.BookingDto;
using SignalR_App.Domain.Entitites;

namespace SignalR_App.Application.Mappings
{
    public class BookingMap : Profile
    {
        public BookingMap()
        {
            CreateMap<Booking, BookingDto>().ReverseMap();
        }
    }
}
