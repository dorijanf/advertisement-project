using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using SharedModels.Messages;

namespace Domain.Services
{
    /// <summary>
    /// Service which publishes messages to the message broker (RabbitMq)
    /// </summary>
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

        /// <summary>
        /// The publish method publishes a message to the message broker where the message
        /// object implements the <see cref="IMessage"/> interface with the id of the message
        /// and the time stamp.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task Publish<T>(T message) where T : IMessage
        {
            try
            {
                await endpoint.Publish(message);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error when publishing: {ex.Message}");
            }
        }
    }
}
