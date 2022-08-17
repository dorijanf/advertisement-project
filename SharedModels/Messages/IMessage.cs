using System;

namespace SharedModels.Messages
{
    /// <summary>
    /// The object which implements this interface can be used as
    /// a a message when sending messages to the RabbitMq message broker.
    /// </summary>
    public interface IMessage
    {
        Guid MessageId { get; set; }
        DateTime CreationDate { get; set; }
    }
}
