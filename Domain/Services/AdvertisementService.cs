using System.Collections.Generic;
using System.Threading.Tasks;
using backend_template.Database.Entities;
using Database;
using Database.Entities;
using Nest;
using SharedModels.Dtos;
using SharedModels.Exceptions;
using SharedModels.Messages;

namespace Domain.Services
{
    /// <summary>
    /// The main domain service for advertisements. Tasked with storing and fetching
    /// data about advertisements to the database.
    /// </summary>
    public class AdvertisementService : IAdvertisementService
    {

        private readonly IPublisherService publisherService;
        private readonly IElasticClient elasticClient;
        private readonly AdvertisementContext dbContext;

        public AdvertisementService(IPublisherService publisherService,
            IElasticClient elasticClient,
            AdvertisementContext dbContext)
        {
            this.publisherService = publisherService;
            this.elasticClient = elasticClient;
            this.dbContext = dbContext;
        }

        /// <summary>
        /// No filtering, simply returning all entities, before going to the db, mapping
        /// the entity to a dto.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AdvertisementDto>> GetAdvertisements(string query, int page,
            int pageSize)
        {
            var response = await elasticClient.SearchAsync<AdvertisementDto>(
                s => s.Query(q => q.QueryString(d => d.Query(query)))
                    .From((page - 1) * pageSize)
                    .Size(pageSize));

            return !response.IsValid ? new List<AdvertisementDto>() : response.Documents;
        }

        /// <summary>
        /// Trying to find a single advertisement by its id. If the advertisement is not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AdvertisementDto> GetAdvertisementById(int id)
        {
            var advertisement = await dbContext.Advertisements.FindAsync(id);

            if (advertisement is null)
            {
                throw new NotFoundException();
            }

            return CreateAdvertisementDto(advertisement);
        }


        /// <summary>
        /// Creates a new advertisement entity from the incoming model after which we attempt
        /// to store it in the database. After storing it we attempt to publish the newly
        /// created advertisement to the message broker.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>id of new advertisement</returns>
        public async Task<int> CreateNewAdvertisement(AdvertisementDto model)
        {
            if (string.IsNullOrEmpty(model.UserEmail))
            {
                throw new BadRequestException("User email is required.");
            }

            if (string.IsNullOrEmpty(model.Title))
            {
                throw new BadRequestException("Title is required.");
            }

            var advertisement = CreateAdvertisementEntity(model);
            dbContext.Add(advertisement);
            await dbContext.SaveChangesAsync();

            await publisherService.Publish(new AdvertisementCreateMessage(CreateAdvertisementDto(advertisement)));

            return advertisement.Id;
        }

        /// <summary>
        /// Creating a new favorite advertisement entry by providing the id of the advertisement
        /// and the email for the user. Since there is no user table/key constraint we are not joining
        /// the user and the advertisement by their id, only the advertisement. In the end publishes
        /// the event to the message broker.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userEmail"></param>
        /// <returns>id of favorite</returns>
        public async Task<int> AddAdvertisementToFavorites(int id, string userEmail)
        {
            var favoriteAdvertisement = new FavoriteAdvertisement
            {
                AdvertisementId = id,
                UserEmail = userEmail
            };

            dbContext.FavoriteAdvertisements.Add(favoriteAdvertisement);

            await dbContext.SaveChangesAsync();

            await publisherService.Publish(new FavoriteCreateMessage(id, userEmail));

            return favoriteAdvertisement.Id;
        }

        /// <summary>
        /// Creates a new <see cref="AdvertisementDto"/> object from the <see cref="Advertisement"/>
        /// entity.
        /// </summary>
        /// <param name="advertisement"></param>
        /// <returns></returns>
        private static AdvertisementDto CreateAdvertisementDto(Advertisement advertisement)
        {
            return new AdvertisementDto
            {
                Id = advertisement.Id,
                Title = advertisement.Title,
                Content = advertisement.Content,
                UserEmail = advertisement.UserEmail
            };
        }

        /// <summary>
        /// Creates a new <see cref="Advertisement"/> object from the <see cref="AdvertisementDto"/>
        /// entity.
        /// </summary>
        /// <param name="advertisement"></param>
        /// <returns></returns>
        private static Advertisement CreateAdvertisementEntity(AdvertisementDto advertisement)
        {
            return new Advertisement
            {
                Title = advertisement.Title,
                Content = advertisement.Content,
                UserEmail = advertisement.UserEmail
            };
        }
    }
}
