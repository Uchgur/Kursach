using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace book_hotel_api.DTOs
{
    public class ImageCreationDTO
    {
        public IFormFile File { get; set; }
        public int? HotelId { get; set; }
        public int? RoomId { get; set; }
    }
}