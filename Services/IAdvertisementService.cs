using SharedModels.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend_template.Services
{
    /// <summary>
    /// Service which performs basic CRUD operations on the advertisement entity.
    /// </summary>
    public interface IAdvertisementService
    {
        /// <summary>
        /// Returns a list of all advertisements.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AdvertisementDto>> GetAdvertisements();

        /// <summary>
        /// Returns a single advertisement transfer object by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AdvertisementDto> GetAdvertisementById(int id);

        /// <summary>
        /// Creates a new advertisement entity and stores it in the database.
        /// The method returns the id of the newly created entity.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> CreateNewAdvertisement(AdvertisementDto model);
    }
}
