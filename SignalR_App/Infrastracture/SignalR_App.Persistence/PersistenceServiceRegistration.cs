using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignalR_App.Domain.Entitites;
using SignalR_App.Persistence.EntityFramework;

namespace SignalR_App.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static void AddPersistenceServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase= true;
                options.Password.RequiredLength= 8;
            })
             .AddEntityFrameworkStores<SignalRDbContext>()
                .AddDefaultTokenProviders();

            services.AddDbContext<SignalRDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            });
        }
    }
}
