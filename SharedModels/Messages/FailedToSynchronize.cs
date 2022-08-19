using System;

namespace SharedModels.Messages
{
    /// <summary>
    /// Failed to synchronize event gets triggered by the advertisement state
    /// machine when indexing to Elastic search fails.
    /// </summary>
    public class FailedToSynchronize : IMessage
    {
        public Guid MessageId { get; set; }
        public DateTime CreationDate { get; set; }
        public int AdvertisementId { get; set; }
    }
}
