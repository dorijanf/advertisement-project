using System.ComponentModel.DataAnnotations;

namespace SharedModels.Dtos
{
    public class AdvertisementDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string UserEmail { get; set; }
    }
}
