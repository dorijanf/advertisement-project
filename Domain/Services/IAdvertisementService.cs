using System.Collections.Generic;
using System.Threading.Tasks;
using SharedModels.Dtos;

namespace Domain.Services
{
    /// <summary>
    /// The main domain service for advertisements. Tasked with storing and fetching
    /// data about advertisements to the database.
    /// </summary>
    public interface IAdvertisementService
    {
        /// <summary>
        /// Returns a list of all advertisements.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AdvertisementDto>> GetAdvertisements(string query, int page, int pageSize);

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
        /// Creates a new favorite advertisement entry in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        Task<int> AddAdvertisementToFavorites(int id, string userEmail);
    }
}
