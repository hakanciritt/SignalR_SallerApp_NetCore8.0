using AutoMapper;
using SignalR_App.Application.Dtos.CategoryDto;
using SignalR_App.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
