using Microsoft.EntityFrameworkCore;
using SignalR_App.Domain.Entitites;

namespace SignalR_App.Persistence.EntityFramework
{
    public class SignalRDbContext : DbContext
    {
        public SignalRDbContext(DbContextOptions<SignalRDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Meta> Metas { get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<TextContent> TextContents{ get; set; }
    }
}
