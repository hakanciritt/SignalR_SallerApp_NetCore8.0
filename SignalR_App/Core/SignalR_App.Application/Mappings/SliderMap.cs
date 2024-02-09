using AutoMapper;
using SignalR_App.Application.Dtos.Dtos;
using SignalR_App.Domain.Entitites;

namespace SignalR_App.Application.Mappings
{
    public class SliderMap : Profile
    {
        public SliderMap()
        {
            CreateMap<Slider, SliderDto>().ReverseMap();   
        }
    }
}
