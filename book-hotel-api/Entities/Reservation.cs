using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace book_hotel_api.Entities
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public bool PayOffline { get; set; }
        [Required]
        public bool PayOnline { get; set; }
        public bool Confirmation { get; set; } = false;
        public bool Canceled { get; set; } = false;
        [Required]
        [ForeignKey("Hotel")]
        public int HotelId { get; set; }
        [Required]
        [ForeignKey("Room")]
        public int RoomId { get; set;}
        [Required]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
