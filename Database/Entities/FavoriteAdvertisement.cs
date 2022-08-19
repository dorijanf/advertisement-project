namespace Database.Entities
{
    public class FavoriteAdvertisement
    {
        public int Id { get; set; }
        public int AdvertisementId { get; set; }
        public string UserEmail { get; set; }
        public virtual Advertisement Advertisement { get; set; }
    }
}
