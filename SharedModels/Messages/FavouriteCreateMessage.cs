using System;

namespace SharedModels.Messages
{
    public class FavouriteCreateMessage
    {
        public FavouriteCreateMessage(int adverisementId, string userEmail)
        {
            MessageId = new Guid();
            AdvertisementId = adverisementId;
            UserEmail = userEmail;
            CreationDate = DateTime.Now;
        }

        public Guid MessageId { get; set; }
        public int AdvertisementId { get; set; }
        public string UserEmail { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
