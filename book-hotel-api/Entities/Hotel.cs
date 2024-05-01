using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace book_hotel_api.Entities
{
    public class Hotel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string ContactInformation { get; set; }
        [Required]
        [StringLength(5000)]
        public string Description { get; set; }
        public string? Image { get; set; }
        public List<Image>? Images { get; set; }
        public List<Room>? Rooms { get; set; }
        public List<Reservation>? Reservations { get; set; }
    }
}
