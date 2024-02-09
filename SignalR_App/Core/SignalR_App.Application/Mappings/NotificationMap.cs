using AutoMapper;
using SignalR_App.Application.Dtos.NotificationDtos;
using SignalR_App.Domain.Entitites;

namespace SignalR_App.Application.Mappings
{
    public class NotificationMap : Profile
    {
        public NotificationMap()
        {
            CreateMap<Notification, NotificationDto>().ReverseMap();
        }
    }
}
