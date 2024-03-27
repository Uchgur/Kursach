using AutoMapper;
using book_hotel_api.DTOs;
using book_hotel_api.Entities;

namespace book_hotel_api.Helpers
{
    public class AutoMapper : Profile
    {
        public AutoMapper() 
        {
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<HotelCreationDTO, Hotel>().ForMember(x => x.Image, options => options.Ignore());

            CreateMap<RoomDTO, Room>().ReverseMap();
            CreateMap<RoomCreationDTO, Room>().ForMember(x => x.Image, options => options.Ignore());

            CreateMap<ImageDTO, Image>().ReverseMap();
            CreateMap<ImageCreationDTO, Image>().ForMember(x => x.File, options => options.Ignore());
        }
    }
}
