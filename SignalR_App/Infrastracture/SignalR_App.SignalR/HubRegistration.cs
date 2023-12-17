using Microsoft.AspNetCore.Builder;
using SignalR_App.SignalR.Hubs;


namespace SignalR_App.SignalR
{
    public static class HubRegistration
    {
        public static void MapHubs(this WebApplication webApplication)
        {
            webApplication.MapHub<MainHub>("/main-hub");
        }
    }
}
