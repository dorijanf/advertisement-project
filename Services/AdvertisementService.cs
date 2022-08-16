﻿using backend_template.Database;
using backend_template.Database.Entities;
using SharedModels.Dtos;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SharedModels.Messages;
using Microsoft.Extensions.Logging;

namespace backend_template.Services
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly IPublishEndpoint endpoint;
        private readonly AdvertisementContext dbContext;
        private readonly ILogger logger;

        public AdvertisementService(IPublishEndpoint endpoint,
            AdvertisementContext dbContext,
            ILogger<AdvertisementService> logger)
        {
            this.endpoint = endpoint;
            this.dbContext = dbContext;
            this.logger = logger;
        }

        /// <summary>
        /// No filtering, simply returning all entities, before going to the db, mapping
        /// the entity to a dto.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AdvertisementDto>> GetAdvertisements()
        {
            // TODO: fetch from elastic 
            throw new NotImplementedException();
        }

        /// <summary>
        /// Trying to find a single advertisement by its id. If the advertisement is not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AdvertisementDto> GetAdvertisementById(int id)
        {
            // TODO: fetch from elastic 
            throw new NotImplementedException();
        }


        public async Task<int> CreateNewAdvertisement(AdvertisementDto model)
        {
            var advertisement = CreateAdvertisementEntity(model);
            dbContext.Add(advertisement);
            await dbContext.SaveChangesAsync();
            await Publish(advertisement);
            return advertisement.Id;
        }

        /// <summary>
        /// Publishes the event to the message broker after inserting the 
        /// entity in the database.
        /// </summary>
        /// <param name="advertisement"></param>
        /// <returns></returns>
        private async Task Publish(Advertisement advertisement)
        {
            try
            {
                await endpoint.Publish(new AdvertisementCreateMessage
                {
                    MessageId = new Guid(),
                    Advertisement = CreateAdvertisementDto(advertisement),
                    CreationDate = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Error when publishing: {ex.Message}");
            }
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
                UserId = advertisement.UserId
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
                UserId = advertisement.UserId
            };
        }
    }
}
