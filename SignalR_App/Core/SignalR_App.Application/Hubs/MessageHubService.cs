using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SignalR_App.Domain.Entitites;

namespace SignalR_App.Application.Hubs
{
    public class MessageHubService(IHubContext<MessageHub> hubContext,
        UserManager<AppUser> userManager) : IMessageHubService
    {
        private readonly IHubContext<MessageHub> _hubContext = hubContext;
        private readonly UserManager<AppUser> _userManager = userManager;



    }
}
