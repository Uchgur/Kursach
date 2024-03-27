using book_hotel_api.Entities;
using System.ComponentModel.DataAnnotations;

namespace book_hotel_api.DTOs
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Beds { get; set; }
        public string Price { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public List<Image>? Images { get; set; }
        public int HotelId { get; set; }
    }
}
