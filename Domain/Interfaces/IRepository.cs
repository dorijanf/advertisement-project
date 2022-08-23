using Database;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    /// <summary>
    /// Generic repository which sits between entity framework and the database.
    /// All database interactions have to go through this repository. The database context
    /// should not be accessed outside of the repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Returns an entity by id. If the entity is not found,
        /// returns null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T?> GetById(int id);

        /// <summary>
        /// Stores a new entry in the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Number of inserted records.</returns>
        Task<int> Insert(T entity);

        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Number of updated records.</returns>
        Task<int> Update(T entity);
    }
}
