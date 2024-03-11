using System.ComponentModel.DataAnnotations;

namespace book_hotel_api.Entities
{
    public class Room
    {
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int Beds { get; set; }
        [Required]
        public string Price { get; set; }
        [StringLength(5000)]
        public string? Description { get; set; }
        public string? Image { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}
