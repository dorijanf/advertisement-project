using System.ComponentModel.DataAnnotations;

namespace SharedModels.Dtos
{
    public class AdvertisementDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserEmail { get; set; }
    }
}
