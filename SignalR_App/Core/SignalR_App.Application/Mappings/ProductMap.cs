using AutoMapper;
using SignalR_App.Application.Dtos.ProductDtos;
using SignalR_App.Domain.Entitites;

namespace SignalR_App.Application.Mappings
{
    public  class ProductMap : Profile
    {
        public ProductMap()
        {
            CreateMap<Product, ProductDto>().ReverseMap();   
        }
    }
}
