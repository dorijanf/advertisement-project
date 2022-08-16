using MassTransit;
using Newtonsoft.Json;
using SharedModels.Messages;
using System;
using System.Threading.Tasks;

namespace backend_template.Services
{
    public class AdvertisementConsumer : IConsumer<AdvertisementCreateMessage>
    {
        public Task Consume(ConsumeContext<AdvertisementCreateMessage> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            Console.WriteLine($"OrderCreated message: {jsonMessage}");
            return null;
        }
    }
}
