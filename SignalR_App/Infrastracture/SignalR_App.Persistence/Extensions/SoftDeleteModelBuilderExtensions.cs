using Microsoft.EntityFrameworkCore;
using SignalR_App.Domain.Entitites;
using System.Linq.Expressions;

namespace SignalR_App.Persistence.Extensions
{
    internal static class SoftDeleteModelBuilderExtensions
    {
        public static ModelBuilder ApplySoftDeleteQueryFilter(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (!typeof(BaseFullEntity).IsAssignableFrom(entityType.ClrType)) continue;

                var param = Expression.Parameter(entityType.ClrType, "entity");
                var prop = Expression.PropertyOrField(param, nameof(BaseFullEntity.IsDeleted));
                var entityNotDeleted = Expression.Lambda(Expression.Equal(prop, Expression.Constant(false)), param);

                entityType.SetQueryFilter(entityNotDeleted);
            }

            return modelBuilder;
        }
    }
}
