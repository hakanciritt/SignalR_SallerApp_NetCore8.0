using AutoMapper;
using SignalR_App.Application.Dtos.MetaDto;
using SignalR_App.Domain.Entitites;

namespace SignalR_App.Application.Mappings
{
    public class MetaMap : Profile
    {
        public MetaMap()
        {
            CreateMap<Meta, MetaDto>().ReverseMap();
        }
    }
}
