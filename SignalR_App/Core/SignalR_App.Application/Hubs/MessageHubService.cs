using Microsoft.AspNetCore.SignalR;

namespace SignalR_App.Application.Hubs
{
    public class MessageHubService(IHubContext<MessageHub> hubContext) : IMessageHubService
    {
        private readonly IHubContext<MessageHub> _hubContext = hubContext;

        public async Task SendMessage(string user, string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
