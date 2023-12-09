using AutoMapper;
using SignalR_App.Application.Dtos.MetaDto;

namespace SignalR_App.Application.Mappings
{
    public class MetaMap : Profile
    {
        public MetaMap()
        {
            CreateMap<MetaMap, MetaDto>().ReverseMap();
        }
    }
}
