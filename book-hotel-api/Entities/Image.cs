using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace book_hotel_api.Entities
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string File { get; set; }
        [ForeignKey("Hotel")]
        public int? HotelId { get; set; }
        [ForeignKey("Room")]
        public int? RoomId { get; set; }
    }
}
