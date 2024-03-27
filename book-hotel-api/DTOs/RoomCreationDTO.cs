using book_hotel_api.Entities;
using System.ComponentModel.DataAnnotations;

namespace book_hotel_api.DTOs
{
    public class RoomCreationDTO
    {
        [Required]
        public string Type { get; set; }
        [Required]
        public int Beds { get; set; }
        [Required]
        public string Price { get; set; }
        [StringLength(5000)]
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public List<Image>? Images { get; set; }
        public int HotelId { get; set; }
    }
}
