using Microsoft.AspNetCore.SignalR;
using SignalR_App.Application.Repositories;
using SignalR_App.Domain.Entitites;

namespace SignalR_App.Application.Hubs;

public class BookingHubService(IHubContext<BookingHub> hubContext,
    IRepository<Notification, int> notificationRepository) : IBookingHubService
{
    private readonly IHubContext<BookingHub> _hubContext = hubContext;
    private readonly IRepository<Notification, int> _notificationRepository = notificationRepository;

    public async Task SendNewReservation(string message, bool isSaveNotification = true)
    {
        //await _hubContext.Clients.Users().SendAsync();

        await _hubContext.Clients.All.SendAsync(ReceiverNames.ReceiveNewReservation, message);

        if (isSaveNotification)
            await _notificationRepository.InsertAsync(new Notification()
            {
                IsRead = false,
                Type = "NewReservation",
                Description = message,
            });

    }
}

