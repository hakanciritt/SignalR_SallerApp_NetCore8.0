using AutoMapper;
using SignalR_App.Application.Dtos.SettingDtos;
using SignalR_App.Domain.Entitites;

namespace SignalR_App.Application.Mappings
{
    public class SettingMap : Profile
    {
        public SettingMap()
        {
            CreateMap<Setting, SettingDto>();
        }
    }
}
