namespace SignalR_App.Application.Hubs
{
    public interface IBookingHubService
    {
        Task SendNewReservation(string message, bool isSaveNotification = true);
    }
}
