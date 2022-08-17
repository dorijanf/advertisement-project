using System;

namespace SharedModels.Messages
{
    /// <summary>
    /// When an advertisement has been added to the favorites of a particular user
    /// this message has to be sent.
    /// </summary>
    public class FavoriteCreateMessage : IMessage
    {
        public FavoriteCreateMessage(int advertisementId, string userEmail)
        {
            MessageId = Guid.NewGuid();
            AdvertisementId = advertisementId;
            UserEmail = userEmail;
            CreationDate = DateTime.Now;
        }

        public Guid MessageId { get; set; }
        public int AdvertisementId { get; set; }
        public string UserEmail { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
