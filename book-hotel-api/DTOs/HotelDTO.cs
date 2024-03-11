namespace book_hotel_api.DTOs
{
    public class HotelDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactInformation { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
    }
}
