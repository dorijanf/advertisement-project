using System.Threading.Tasks;
using Database;
using MassTransit;
using SharedModels.Messages;

namespace Domain.Subscribers
{
    /// <summary>
    /// Mass transit consumer service which consumes the <see cref="MarkAsFailedToSynchronize"/>
    /// and gets triggered whenever indexing to elastic search fails.
    /// </summary>
    public class FailedToSynchronizeConsumer : IConsumer<MarkAsFailedToSynchronize>
    {
        private readonly AdvertisementContext dbContext;

        public FailedToSynchronizeConsumer(AdvertisementContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// When we receive the message we need to store it in elastic search and in case the operation
        /// fails we need to rollback and mark the document as failed to synchronize.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<MarkAsFailedToSynchronize> context)
        {
            var advertisement = await dbContext.Advertisements.FindAsync(context.Message.AdvertisementId);

            if (advertisement is not null)
            {
                advertisement.FailedToSync = true;
                dbContext.Update(advertisement);
               await dbContext.SaveChangesAsync();
            }
        }
    }
}
