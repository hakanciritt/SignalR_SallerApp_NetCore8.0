using AutoMapper;
using SignalR_App.Application.Dtos.OrderDtos;
using SignalR_App.Domain.Entitites;

namespace SignalR_App.Application.Mappings
{
    public class OrderMap : Profile
    {
        public OrderMap()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
        }
    }
}
