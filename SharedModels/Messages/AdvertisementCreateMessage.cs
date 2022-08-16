using SharedModels.Dtos;
using System;

namespace SharedModels.Messages
{
    public class AdvertisementCreateMessage
    {
        public AdvertisementCreateMessage(AdvertisementDto advertisement)
        {
            MessageId = new Guid();
            Advertisement = advertisement;
            CreationDate = DateTime.Now;
        }

        public Guid MessageId { get; set; }
        public AdvertisementDto Advertisement { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
