using book_hotel_api.Entities;
using book_hotel_api.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace book_hotel_api.DTOs
{
    public class HotelCreationDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string ContactInformation { get; set; }
        [Required]
        [StringLength(5000)]
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
        public List<Image>? Images { get; set; }
        public List<Room>? Rooms { get; set; }
    }
}
