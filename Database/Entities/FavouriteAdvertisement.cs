namespace backend_template.Database.Entities
{
    public class FavouriteAdvertisement
    {
        public int Id { get; set; }
        public int AdvertisementId { get; set; }
        public int UserId { get; set; }
    }
}
