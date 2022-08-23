using Database;
using Domain.Interfaces;

namespace Infrastructure.Repository
{
    /// <summary>
    /// Generic repository which sits between entity framework and the database.
    /// All database interactions have to go through this repository. The database context
    /// should not be accessed outside of the repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AdvertisementContext dbContext;

        public Repository(AdvertisementContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T?> GetById(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public async Task<int> Insert(T entity)
        {
            dbContext.Set<T>().Add(entity);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
            return await dbContext.SaveChangesAsync();
        }
    }
}
