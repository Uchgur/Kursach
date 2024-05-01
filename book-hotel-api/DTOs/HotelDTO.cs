using book_hotel_api.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace book_hotel_api.DTOs
{
    public class HotelDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ContactInformation { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public List<Image>? Images { get; set; }
        public List<Room>? Rooms { get; set; }
    }
}
