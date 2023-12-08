using SignalR_App.Persistence.EntityFramework;

namespace SignalR_App.Application.Repositories
{
    public interface IRepository<TEntity, TPrimaryKey> where TEntity : class where TPrimaryKey : struct
    {
        void Insert(TEntity entity);
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity> InsertAndGetAsync(TEntity entity);
        TEntity Get(TPrimaryKey id);
        Task<TEntity> GetAsync(TPrimaryKey id);
        void Delete(TEntity entity);
        Task DeleteAsync(TPrimaryKey id);
        void Update(TEntity entity);
        IQueryable<TEntity> GetAll();
        void SaveChanges();
        Task SaveChangesAsync();
        SignalRDbContext GetDbContext();
    }
}
