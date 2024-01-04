using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SignalR_App.Application.Hubs;
using SignalR_App.Application.Redis;
using SignalR_App.Application.Repositories;
using SignalR_App.Application.Services.Abstracts;
using SignalR_App.Application.Services.Concretes;
using SignalR_App.Application.WebServices;
using SignalR_App.Domain.Entitites;
using SignalR_App.Persistence.EntityFramework;
using StackExchange.Redis;

namespace SignalR_App.Application
{
    public static class ApplicationServiceRegistration
    {
        public static void AddApplicationServiceRegistration(this IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            services.AddIdentity<AppUser,AppRole>()
                .AddEntityFrameworkStores<SignalRDbContext>()
                .AddDefaultTokenProviders();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379,abortConnect=false ";
                options.InstanceName = "master";
            });

            services.AddSingleton(provider =>
            {
                var connection = ConnectionMultiplexer.Connect("localhost:6379,abortConnect=false ");
                return new RedisConfiguration(connection);
            });
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<ITestimonailService, TestimonailService>();
            services.AddScoped<ITextContentService, TextContentService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ISliderService, SliderService>();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IWebService, WebService>();
            services.AddScoped<IBasketWebService, BasketWebService>();

            #region Hubs

            services.AddScoped<IBookingHubService, BookingHubService>();

            #endregion


        }
    }
}
