using AutoMapper;
using SignalR_App.Application.Dtos;
using SignalR_App.Domain.Entitites;

namespace SignalR_App.Application.Mappings
{
    public class BaseFullEntityMap : Profile
    {
        public BaseFullEntityMap()
        {
            CreateMap<BaseFullEntity,BaseFullEntityDto>().ReverseMap();
            CreateMap(typeof(BaseFullEntity<>), typeof(BaseFullEntityDto<>)).ReverseMap();

        }
    }
}
