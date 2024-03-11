using AutoMapper;
using book_hotel_api.DTOs;
using book_hotel_api.Entities;

namespace book_hotel_api.Helpers
{
    public class AutoMapper : Profile
    {
        public AutoMapper() 
        {
            CreateMap<HotelDTO, Hotel>().ReverseMap();
            CreateMap<HotelCreationDTO, Hotel>();
        }
    }
}
