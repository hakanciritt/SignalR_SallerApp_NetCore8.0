using Microsoft.EntityFrameworkCore;
using SignalR_App.Application.Dtos.NotificationDtos;
using SignalR_App.Application.Mappings;
using SignalR_App.Application.Repositories;
using SignalR_App.Application.Services.Abstracts;
using SignalR_App.Domain.Entitites;

namespace SignalR_App.Application.Services.Concretes
{
    public class NotificationService(IRepository<Notification, int> notificationRepository) : INotificationService
    {
        private readonly IRepository<Notification, int> _notificationRepository = notificationRepository;

        public async Task<List<NotificationDto>> GetAllNotifications()
        {
            var result = await _notificationRepository.GetAll()
                .OrderByDescending(c => c.CreatedDate).Take(40).ToListAsync();

            return ObjectMapper.Map.Map<List<NotificationDto>>(result);
        }
    }
}
