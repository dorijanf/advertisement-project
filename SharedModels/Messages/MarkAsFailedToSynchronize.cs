using System;

namespace SharedModels.Messages
{
    /// <summary>
    /// When an advertisement fails to get indexed in Elastic search
    /// this message gets sent and consumed after which the entity
    /// is marked as failed to synchronize.
    /// </summary>
    public class MarkAsFailedToSynchronize : IMessage
    {
        public MarkAsFailedToSynchronize(int advertisementId)
        {
            MessageId = Guid.NewGuid();
            AdvertisementId = advertisementId;
            CreationDate = DateTime.Now;
        }
        public int AdvertisementId { get; set; }
        public Guid MessageId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
