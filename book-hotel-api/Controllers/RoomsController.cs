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
    [ApiController]
    [Route("api/hotel/rooms")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsHotelOwner")]
    public class RoomsController : ControllerBase
    {
        private readonly ILogger<RoomsController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly string containerName = "rooms";

        public RoomsController(ILogger<RoomsController> logger, ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager, IFileStorageService fileStorageService)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<RoomDTO>>> Get(int hotelId)
        {
            var queryable = _context.Rooms.AsQueryable().Where(x => x.HotelId == hotelId);
            var rooms = await queryable.OrderBy(x => x.Price).ToListAsync();
            return _mapper.Map<List<RoomDTO>>(rooms);
        }

        [HttpGet("room/{Id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<RoomDTO>> GetRoom(int id)
        {
            var room = await _context.Rooms.Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == id);

            if (room == null)
            {
                return NotFound();
            }

            return _mapper.Map<RoomDTO>(room);
        }

        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<ActionResult<List<RoomDTO>>> Filter([FromQuery] FilterRoomsDTO filterRoomsDTO, int hotelId)
        {
            var roomsQueryable = _context.Rooms.AsQueryable().Where(x => x.HotelId == hotelId);

            if (!string.IsNullOrEmpty(filterRoomsDTO.Type))
            {
                roomsQueryable = roomsQueryable.Where(x => x.Type.Contains(filterRoomsDTO.Type));
            }

            if (!string.IsNullOrEmpty(filterRoomsDTO.Price))
            {
                roomsQueryable = roomsQueryable.Where(x => x.Price.Contains(filterRoomsDTO.Price));
            }

            var rooms = await roomsQueryable.OrderBy(x => x.Type).ToListAsync();
            return _mapper.Map<List<RoomDTO>>(rooms);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Post([FromForm] RoomCreationDTO roomCreationDTO)
        {
            var claim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email");

            if (claim == null)
            {
                return BadRequest("You are not logged in");
            }

            var email = claim.Value;
            var user = await _userManager.FindByEmailAsync(email);

            var room = _mapper.Map<Room>(roomCreationDTO);
            room.UserId = user.Id;

            if (roomCreationDTO.Image != null)
            {
                room.Image = await _fileStorageService.SaveFile(containerName, roomCreationDTO.Image);
            }

            _context.Add(room);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("edit/{Id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] RoomCreationDTO roomCreationDTO)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == id);

            if (room == null)
            {
                return NotFound();
            }

            room = _mapper.Map(roomCreationDTO, room);

            if (roomCreationDTO.Image != null)
            {
                room.Image = await _fileStorageService.EditFile(containerName, roomCreationDTO.Image, room.Image);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("delete/{Id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == id);

            if (room == null)
            {
                return NotFound();
            }

            _context.Remove(room);
            await _context.SaveChangesAsync();
            if (room.Image != null)
            {
                await _fileStorageService.DeleteFile(room.Image, containerName);
            }

            return NoContent();
        }
    }
}
