namespace SharedModels.Utils
{
    public class RabbitMqSettings
    {
        public string HostName { get; set; }
        public string VirtualHost { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AdvertisementAddedQueue { get; set; }
        public string FavoriteAddedQueue { get; set; }
    }
}
