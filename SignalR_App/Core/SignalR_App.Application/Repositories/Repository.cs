using Microsoft.EntityFrameworkCore;
using SignalR_App.Persistence.EntityFramework;

namespace SignalR_App.Application.Repositories
{
    public class Repository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : class where TPrimaryKey : struct
    {
        private readonly SignalRDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(SignalRDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }
        public SignalRDbContext GetDbContext() => _dbContext;

        public virtual void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual async Task DeleteAsync(TPrimaryKey id)
        {
            var entity = await GetAsync(id);
            if (entity != null) _dbSet.Remove(entity);
        }

        public virtual TEntity Get(TPrimaryKey id)
        {
            return _dbSet.Find(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public virtual async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual async Task<TEntity> InsertAndGetAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync(); // Veritabanına hemen kaydet
            return entity;
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            var result = await _dbSet.AddAsync(entity);
            return result.Entity;
        }

        public virtual void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public virtual async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

    }
}
