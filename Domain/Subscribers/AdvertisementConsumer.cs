using System.Threading.Tasks;
using Database;
using MassTransit;
using Nest;
using SharedModels.Exceptions;
using SharedModels.Messages;

namespace Domain.Subscribers
{
    /// <summary>
    /// Mass transit consumer service which consumes the <see cref="AdvertisementCreateMessage"/>
    /// and gets triggered whenever a new advertisement gets stored in the database.
    /// </summary>
    public class AdvertisementConsumer : IConsumer<AdvertisementCreateMessage>
    {
        private readonly IElasticClient elasticClient;
        private readonly AdvertisementContext dbContext;

        public AdvertisementConsumer(
            IElasticClient elasticClient,
            AdvertisementContext dbContext)
        {
            this.elasticClient = elasticClient;
            this.dbContext = dbContext;
        }

        /// <summary>
        /// When we receive the message we need to store it in elastic search and in case the operation
        /// fails we need to rollback and mark the document as failed to synchronize.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<AdvertisementCreateMessage> context)
        {
            var retryCount = context.GetRetryAttempt();
            if (retryCount < 2)
            {
                var pingResponse = await elasticClient.PingAsync();

                if (pingResponse.IsValid)
                {
                    var indexResponse = await elasticClient.IndexDocumentAsync(context.Message.Advertisement);
                    if (!indexResponse.IsValid)
                    {
                        throw new ElasticSearchError(indexResponse.OriginalException.Message);
                    }
                }
                else
                {
                    throw new ElasticSearchError(pingResponse.OriginalException.Message);
                }
            }
            else
            {
                await MarkAsFailedToSynchronize(context.Message.Advertisement.Id);
            }

        }

        /// <summary>
        /// Mark the advertisement as failed to synchronize in database.
        /// </summary>
        /// <returns></returns>
        private async Task MarkAsFailedToSynchronize(int id)
        {
            var advertisement = await dbContext.Advertisements.FindAsync(id);

            if (advertisement is not null)
            {
                advertisement.FailedToSync = advertisement.FailedToSync is not true;
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
