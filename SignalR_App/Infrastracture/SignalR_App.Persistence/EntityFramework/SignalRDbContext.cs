using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SignalR_App.Domain.Entitites;
using SignalR_App.Persistence.Extensions;

namespace SignalR_App.Persistence.EntityFramework
{
    public class SignalRDbContext : IdentityDbContext<AppUser, AppRole, long>
    {
        public SignalRDbContext(DbContextOptions<SignalRDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Meta> Metas { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<TextContent> TextContents { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Basket> Basket { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<MenuTable> MenuTables { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<AppPermission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplySoftDeleteQueryFilter();
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            FiilInTheEntityStates();
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            FiilInTheEntityStates();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            FiilInTheEntityStates();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void FiilInTheEntityStates()
        {
            var entities = ChangeTracker.Entries().ToList();
            foreach (var item in entities)
            {
                if (item.Entity is BaseEntity result)
                {
                    if (item.State == EntityState.Added && result.CreatedDate == default)
                    {
                        result.CreatedDate = DateTime.Now;
                    }
                    else if (item.State == EntityState.Modified)
                    {
                        result.LastModificationDate = DateTime.Now;
                    }
                }
                if (item.Entity is BaseFullEntity fullEntityResult)
                {
                    if (item.State == EntityState.Deleted)
                    {
                        fullEntityResult.IsDeleted = true;
                        fullEntityResult.DelationTime = DateTime.Now;
                        item.State = EntityState.Modified;
                    }
                    else if (item.State == EntityState.Added)
                    {
                        fullEntityResult.IsDeleted = false;
                        fullEntityResult.DelationTime = null;
                    }
                }
            }
        }
    }
}
