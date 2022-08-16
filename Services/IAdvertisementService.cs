using SharedModels.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend_template.Services
{
    /// <summary>
    /// The main domain service for advertisements. Tasked with storing and fetching
    /// data about adveritsements to the database.
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
        /// Creates a new advertisement and stores it in the database.
        /// The method returns the id of the newly created entity.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> CreateNewAdvertisement(AdvertisementDto model);

        /// <summary>
        /// Creates a new favourite advertisement entry in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        Task<int> AddAdvertisementToFavourites(int id, string userEmail);
    }
}
