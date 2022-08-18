using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nest;
using SharedModels.Messages;

namespace Domain.Subscribers
{
    /// <summary>
    /// Mass transit consumer service which consumes the <see cref="AdvertisementCreateMessage"/>
    /// and gets triggered whenever a new advertisement gets stored in the database.
    /// </summary>
    public class AdvertisementConsumer : IConsumer<AdvertisementCreateMessage>
    {
        private readonly ILogger<AdvertisementConsumer> logger;
        private readonly IElasticClient elasticClient;

        public AdvertisementConsumer(ILogger<AdvertisementConsumer> logger, 
            IElasticClient elasticClient)
        {
            this.logger = logger;
            this.elasticClient = elasticClient;
        }

        /// <summary>
        /// When we receive the message we need to store it in elastic search and in case the operation
        /// fails we need to rollback and mark the document as failed to synchronize.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<AdvertisementCreateMessage> context)
        {
            try
            {
                await elasticClient.IndexDocumentAsync(context.Message.Advertisement);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error when consuming advertisement create message: {ex.Message}");
            }
        }
    }
}
