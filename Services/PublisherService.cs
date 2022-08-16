using MassTransit;
using Microsoft.Extensions.Logging;
using SharedModels.Messages;
using System;
using System.Threading.Tasks;

namespace backend_template.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublishEndpoint endpoint;
        private readonly ILogger<PublisherService> logger;

        public PublisherService(IPublishEndpoint endpoint,
            ILogger<PublisherService> logger)
        {
            this.endpoint = endpoint;
            this.logger = logger;
        }

        public async Task Publish<T>(T message)
        {
            try
            {
                await endpoint.Publish(message);
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Error when publishing: {ex.Message}");
            }
        }
    }
}
