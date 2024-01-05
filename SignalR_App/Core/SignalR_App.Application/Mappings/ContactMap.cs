using AutoMapper;
using SignalR_App.Application.Dtos.ContactDtos;
using SignalR_App.Domain.Entitites;

namespace SignalR_App.Application.Mappings
{
    public class ContactMap : Profile
    {
        public ContactMap()
        {
            CreateMap<ContactDto, Contact>().ReverseMap();
        }
    }
}
