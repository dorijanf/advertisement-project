using SharedModels.Dtos;
using System;

namespace SharedModels.Messages
{
    public class AdvertisementCreateMessage
    {
        public Guid MessageId { get; set; }
        public AdvertisementDto Advertisement { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
