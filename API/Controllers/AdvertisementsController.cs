using System.Collections.Generic;
using System.Threading.Tasks;
using backend_template.Domain.Services;
using backend_template.Services;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Dtos;

namespace backend_template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementsController : ControllerBase
    {
        private readonly IAdvertisementService advertisementService;

        public AdvertisementsController(IAdvertisementService advertisementService)
        {
            this.advertisementService = advertisementService;
        }

        /// <summary>
        /// Gets a list of all advertisements containing the title, id and user id.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<AdvertisementDto>> GetAdvertisements() =>
            await advertisementService.GetAdvertisements();

        /// <summary>
        /// Gets a single AdvertisementDto object by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<AdvertisementDto> GetAdvertisementById(int id) =>
            await advertisementService.GetAdvertisementById(id);

        /// <summary>
        /// Creates a new advertisement and stores it in the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>id of the newly created advertisement</returns>
        [HttpPost]
        public async Task<int> CreateNewAdvertisement([FromBody] AdvertisementDto model) =>
            await advertisementService.CreateNewAdvertisement(model);

        /// <summary>
        /// Finds an advertisement by id and stores it in the database together
        /// with an email from the user which added the advertisement to their favourites.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>id of the newly created advertisement</returns>
        [HttpPost("{id}")]
        public async Task<int> AddAdvertisementToFavourites([FromRoute] int id, [FromBody] string userEmail) =>
            await advertisementService.AddAdvertisementToFavourites(id, userEmail);
    }
}
