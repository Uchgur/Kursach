using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace book_hotel_api.Entities
{
    public class Room
    {
        [Key]
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
        public List<Image>? Images { get; set; }
        public List<Reservation>? Reservations { get; set; }
        [ForeignKey("Hotel")]
        public int HotelId { get; set; }
        [Required]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
