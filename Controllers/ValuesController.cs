using System.Collections.Generic;
using System.Threading.Tasks;
using backend_template.Services;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Dtos;

namespace backend_template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IAdvertisementService advertisementService;

        public ValuesController(IAdvertisementService advertisementService)
        {
            this.advertisementService = advertisementService;
        }

        /// <summary>
        /// Gets a list of all advertisements containing the title, id and user id.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<AdvertisementDto>> Get() =>
            await advertisementService.GetAdvertisements();

        /// <summary>
        /// Gets a single AdvertisementDto object by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<AdvertisementDto> Get(int id) =>
            await advertisementService.GetAdvertisementById(id);

        /// <summary>
        /// Creates a new advertisement and stores it in the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>id of the newly created advertisement</returns>
        [HttpPost]
        public async Task<int> Post([FromBody] AdvertisementDto model) =>
            await advertisementService.CreateNewAdvertisement(model);

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
