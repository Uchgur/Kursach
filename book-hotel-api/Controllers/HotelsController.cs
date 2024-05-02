using AutoMapper;
using book_hotel_api.DTOs;
using book_hotel_api.Entities;
using book_hotel_api.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly string containerName = "hotels";

        public HotelsController(ILogger<HotelsController> logger, ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager, IFileStorageService fileStorageService)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
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

        [HttpGet("myhotels")]
        public async Task<ActionResult<List<HotelDTO>>> GetMyHotels()
        {
            var claim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email");

            if (claim == null)
            {
                return BadRequest("You are not logged in");
            }

            var email = claim.Value;
            var user = await _userManager.FindByEmailAsync(email);

            var queryable = _context.Hotels.AsQueryable().Where(x => x.UserId == user.Id);
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

        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<ActionResult<List<HotelDTO>>> Filter([FromQuery] FilterHotelsDTO filterHotelsDTO)
        {
            var hotelsQueryable = _context.Hotels.AsQueryable();

            if (!string.IsNullOrEmpty(filterHotelsDTO.Country))
            {
                hotelsQueryable = hotelsQueryable.Where(x => x.Country.Contains(filterHotelsDTO.Country));
            }

            if (!string.IsNullOrEmpty(filterHotelsDTO.City))
            {
                hotelsQueryable = hotelsQueryable.Where(x => x.City.Contains(filterHotelsDTO.City));
            }

            var hotels = await hotelsQueryable.OrderBy(x => x.Name).ToListAsync();
            return _mapper.Map<List<HotelDTO>>(hotels);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Post([FromForm] HotelCreationDTO hotelCreationDTO)
        {
            var claim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email");

            if (claim == null)
            {
                return BadRequest("You are not logged in");
            }

            var email = claim.Value;
            var user = await _userManager.FindByEmailAsync(email);

            var hotel = _mapper.Map<Hotel>(hotelCreationDTO);
            hotel.UserId = user.Id;

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
