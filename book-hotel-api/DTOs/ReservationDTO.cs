using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace book_hotel_api.DTOs
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Confirmation { get; set; }
        public bool Canceled { get; set; }
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public string UserId { get; set; }
    }
}
