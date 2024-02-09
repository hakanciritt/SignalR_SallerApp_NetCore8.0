using AutoMapper;
using SignalR_App.Application.Dtos.OrderItemDtos;
using SignalR_App.Domain.Entitites;

namespace SignalR_App.Application.Mappings
{
    public class OrderItemMap : Profile
    {
        public OrderItemMap()
        {
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        }
    }
}
