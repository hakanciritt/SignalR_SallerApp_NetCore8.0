using AutoMapper;
using SignalR_App.Application.Dtos.BasketDtos;
using SignalR_App.Application.Dtos.MenuTableDtos;
using SignalR_App.Domain.Entitites;

namespace SignalR_App.Application.Mappings
{
    public class BasketMap : Profile
    {
        public BasketMap()
        {
            CreateMap<Basket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            CreateMap<MenuTable, MenuTableDto>().ReverseMap();
        }
    }
}
