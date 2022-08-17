using System.Threading.Tasks;
using MassTransit;
using SharedModels.Messages;

namespace Domain.Services
{
    public class AdvertisementConsumer : IConsumer<AdvertisementCreateMessage>
    {
        public Task Consume(ConsumeContext<AdvertisementCreateMessage> context)
        {
            return null;
        }
    }
}
