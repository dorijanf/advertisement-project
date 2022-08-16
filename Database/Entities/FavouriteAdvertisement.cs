namespace backend_template.Database.Entities
{
    public class FavouriteAdvertisement
    {
        public int Id { get; set; }
        public int AdvertisementId { get; set; }
        public string UserEmail { get; set; }
        public virtual Advertisement Advertisement { get; set; }
    }
}
