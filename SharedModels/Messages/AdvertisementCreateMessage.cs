using SharedModels.Dtos;
using System;

namespace SharedModels.Messages
{
    /// <summary>
    /// When an advertisement gets created and stored in the database, this
    /// message has to be sent.
    /// </summary>
    public class AdvertisementCreateMessage : IMessage
    {
        public AdvertisementCreateMessage(AdvertisementDto advertisement)
        {
            MessageId = Guid.NewGuid();
            Advertisement = advertisement;
            CreationDate = DateTime.Now;
        }

        public Guid MessageId { get; set; }
        public AdvertisementDto Advertisement { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
