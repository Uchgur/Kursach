using System.ComponentModel.DataAnnotations;

namespace book_hotel_api.DTOs
{
    public class FilterRoomsDTO
    {
        public string? Type { get; set; }
        public string? Price { get; set; }
    }
}
