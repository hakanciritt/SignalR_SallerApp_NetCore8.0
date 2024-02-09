using Microsoft.Extensions.DependencyInjection;
using SignalR_App.SignalR.Hubs;

namespace SignalR_App.SignalR
{
    public static class SignalRServiceRegistration
    {
        public static void AddSignalRServiceRegistration(this IServiceCollection services)
        {
             
            services.AddTransient<MainHub>();
            services.AddSignalR();
        }
    }
}
