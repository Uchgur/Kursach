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
        public bool Confirmation { get; set; }
        public bool Canceled { get; set; }
        [Required]
        public int HotelId { get; set; }
        [Required]
        public int RoomId { get; set; }
    }
}
