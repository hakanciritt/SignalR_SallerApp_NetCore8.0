using AutoMapper;
using SignalR_App.Application.Dtos;
using SignalR_App.Domain.Entitites;

namespace SignalR_App.Application.Mappings
{
    public class BaseEntityMap : Profile
    {
        public BaseEntityMap()
        {
            CreateMap<BaseEntity, BaseEntityDto>().ReverseMap();  
            CreateMap(typeof(BaseEntity<>),typeof(BaseEntityDto<>)).ReverseMap();  
        }
    }
}
