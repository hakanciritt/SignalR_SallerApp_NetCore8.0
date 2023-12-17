using SignalR_App.Application.Repositories;
using SignalR_App.Application.Services.Abstracts;
using SignalR_App.Domain.Entitites;

namespace SignalR_App.Application.Services.Concretes
{
    public class OrderService(IRepository<Order, Guid> orderRepository) : IOrderService
    {
        private readonly IRepository<Order, Guid> _orderRepository = orderRepository;

    }
}
