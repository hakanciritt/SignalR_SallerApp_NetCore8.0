using AutoMapper;
using SignalR_App.Application.Dtos.CategoryDto;
using SignalR_App.Domain.Entitites;

namespace SignalR_App.Application.Mappings
{
    public class CategoryMap : Profile
    {
        public CategoryMap()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
