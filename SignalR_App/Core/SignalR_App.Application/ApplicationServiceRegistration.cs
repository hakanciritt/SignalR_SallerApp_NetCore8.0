using Microsoft.Extensions.DependencyInjection;
using SignalR_App.Application.Repositories;
using SignalR_App.Application.Services.Abstracts;
using SignalR_App.Application.Services.Concretes;

namespace SignalR_App.Application
{
    public static class ApplicationServiceRegistration
    {
        public static void AddApplicationServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            services.AddScoped<IBookingService,BookingService>();
            services.AddScoped<ICategoryService,CategoryService>();
            services.AddScoped<IDiscountService,DiscountService>();
            services.AddScoped<IProductService,ProductService>();
            services.AddScoped<ISettingService,SettingService>();
            services.AddScoped<ITestimonailService,TestimonailService>();
            services.AddScoped<ITextContentService,TextContentService>();
        }
    }
}
