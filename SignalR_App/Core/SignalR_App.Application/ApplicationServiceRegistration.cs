using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SignalR_App.Application.Repositories;

namespace SignalR_App.Application
{
    public static class ApplicationServiceRegistration
    {
        public static void AddApplicationServiceRegistration(this IServiceCollection services)
        {
            services.TryAddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        }
    }
}
