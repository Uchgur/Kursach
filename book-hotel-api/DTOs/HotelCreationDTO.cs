using System.ComponentModel.DataAnnotations;

namespace book_hotel_api.DTOs
{
    public class HotelCreationDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string ContactInformation { get; set; }
        [Required]
        [StringLength(5000)]
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
    }
}
