using System.Threading.Tasks;
using SharedModels.Messages;

namespace Domain.Interfaces
{
    /// <summary>
    /// Service which publishes messages to the message broker (RabbitMq)
    /// </summary>
    public interface IPublisherService
    {
        Task Publish<T>(T message) where T : IMessage;
    }
}
