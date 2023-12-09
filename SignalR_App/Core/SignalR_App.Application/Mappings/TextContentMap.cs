using AutoMapper;
using SignalR_App.Application.Dtos.TextContentDtos;
using SignalR_App.Domain.Entitites;

namespace SignalR_App.Application.Mappings
{
    public class TextContentMap : Profile
    {
        public TextContentMap()
        {
            CreateMap<TextContent, TextContentDto>().ReverseMap();
        }
    }
}
