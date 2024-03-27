using System.ComponentModel.DataAnnotations.Schema;

namespace book_hotel_api.DTOs
{
    public class ImageDTO
    {
        public int Id { get; set; }
        public string File { get; set; }
        public int? HotelId { get; set; }
        public int? RoomId { get; set; }
    }
}
