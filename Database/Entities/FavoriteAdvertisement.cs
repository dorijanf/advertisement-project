namespace Database.Entities
{
    public class FavoriteAdvertisement : BaseEntity
    {
        public int Id { get; set; }
        public int AdvertisementId { get; set; }
        public string UserEmail { get; set; }
        public virtual Advertisement Advertisement { get; set; }
    }
}
