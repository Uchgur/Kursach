using AutoMapper;
using book_hotel_api.DTOs;
using book_hotel_api.Entities;
using book_hotel_api.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace book_hotel_api.Controllers
{
    [Route("api/hotels")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsHotelOwner")]
    public class HotelsController : ControllerBase
    {
        private readonly ILogger<HotelsController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly string containerName = "hotels";

        public HotelsController(ILogger<HotelsController> logger, ApplicationDbContext context, IMapper mapper, IFileStorageService fileStorageService)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<HotelDTO>>> Get()
        {
            var queryable = _context.Hotels.AsQueryable();
            var hotels = await queryable.OrderBy(x => x.Name).ToListAsync();
            return _mapper.Map<List<HotelDTO>>(hotels);
        }

        [HttpGet("hotel/{Id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<HotelDTO>> Get(int id)
        {
            var hotel = await _context.Hotels.Include(x => x.Rooms).Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == id);

            if (hotel == null)
            {
                return NotFound();
            }

            return _mapper.Map<HotelDTO>(hotel);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Post([FromForm] HotelCreationDTO hotelCreationDTO)
        {
            var hotel = _mapper.Map<Hotel>(hotelCreationDTO);

            if (hotelCreationDTO.Image != null)
            {
                hotel.Image = await _fileStorageService.SaveFile(containerName, hotelCreationDTO.Image);
            }

            _context.Add(hotel);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("edit/{Id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] HotelCreationDTO hotelCreationDTO)
        {
            var hotel = await _context.Hotels.FirstOrDefaultAsync(x => x.Id == id);

            if (hotel == null)
            {
                return NotFound();
            }

            hotel = _mapper.Map(hotelCreationDTO, hotel);

            if (hotelCreationDTO.Image != null)
            {
                hotel.Image = await _fileStorageService.EditFile(containerName, hotelCreationDTO.Image, hotel.Image);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("delete/{Id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var hotel = await _context.Hotels.FirstOrDefaultAsync(x => x.Id == id);

            if (hotel == null)
            {
                return NotFound();
            }

            _context.Remove(hotel);
            await _context.SaveChangesAsync();
            if (hotel.Image != null)
            {
                await _fileStorageService.DeleteFile(hotel.Image, containerName);
            }

            return NoContent();
        }
    }
}
