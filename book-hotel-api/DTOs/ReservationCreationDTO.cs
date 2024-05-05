using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace book_hotel_api.DTOs
{
    public class ReservationCreationDTO
    {
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
        public bool Confirmation { get; set; }
        public bool Canceled { get; set; }
        [Required]
        public int HotelId { get; set; }
        [Required]
        public int RoomId { get; set; }
    }
}
