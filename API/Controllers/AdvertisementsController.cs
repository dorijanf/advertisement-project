using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Dtos;

namespace backend_template.Controllers
{
    /// <summary>
    /// Handles all requests regarding the advertisement entity.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementsController : ControllerBase
    {
        private readonly IAdvertisementService advertisementService;

        /// <param name="advertisementService"></param>
        public AdvertisementsController(IAdvertisementService advertisementService)
        {
            this.advertisementService = advertisementService;
        }

        /// <summary>
        /// Gets a list of all advertisements.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<AdvertisementDto>> GetAdvertisements([FromQuery] string query, [FromQuery] int page = 1, [FromQuery] int pageSize = 50) =>
            await advertisementService.GetAdvertisements(query, page, pageSize);

        /// <summary>
        /// Gets a single advertisement object by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<AdvertisementDto> GetAdvertisementById(int id) =>
            await advertisementService.GetAdvertisementById(id);

        /// <summary>
        /// Creates a new advertisement.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>id of the newly created advertisement</returns>
        [HttpPost]
        public async Task<int> CreateNewAdvertisement([FromBody] AdvertisementDto model) =>
            await advertisementService.CreateNewAdvertisement(model);

        /// <summary>
        /// Adds an advertisement to a favorite for a user email which is provided in the body of the request.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userEmail"></param>
        /// <returns>id of the newly created advertisement</returns>
        [HttpPost("{id:int}")]
        public async Task<int> AddAdvertisementToFavorites([FromRoute] int id, [FromBody] string userEmail) =>
            await advertisementService.AddAdvertisementToFavorites(id, userEmail);
    }
}
