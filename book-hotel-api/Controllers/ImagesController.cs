using AutoMapper;
using book_hotel_api.DTOs;
using book_hotel_api.Entities;
using book_hotel_api.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace book_hotel_api.Controllers
{
    [ApiController]
    [Route("api/images")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsHotelOwner")]
    public class ImagesController : ControllerBase
    {
        private readonly ILogger<HotelsController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly string containerName = "images";

        public ImagesController(ILogger<HotelsController> logger, ApplicationDbContext context, IMapper mapper, IFileStorageService fileStorageService)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<ImageDTO>>> Get(int id, bool isRoom)
        {
            if (isRoom == false)
            {
                var queryable = _context.Image.AsQueryable().Where(x => x.HotelId == id);
                var images = await queryable.OrderBy(x => x.Id).ToListAsync();
                return _mapper.Map<List<ImageDTO>>(images);
            } 
            else
            {
                var queryable = _context.Image.AsQueryable().Where(x => x.RoomId == id);
                var images = await queryable.OrderBy(x => x.Id).ToListAsync();
                return _mapper.Map<List<ImageDTO>>(images);
            } 
        }

        [HttpPost("create")]
        public async Task<ActionResult> Post([FromForm] ImageCreationDTO imageCreationDTO)
        {
            var image = _mapper.Map<Image>(imageCreationDTO);

            image.File = await _fileStorageService.SaveFile(containerName, imageCreationDTO.File);

            _context.Add(image);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("delete/{Id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var image = await _context.Image.FirstOrDefaultAsync(x => x.Id == id);

            if (image == null)
            {
                return NotFound();
            }

            _context.Remove(image);
            await _context.SaveChangesAsync();
            if (image.File != null)
            {
                await _fileStorageService.DeleteFile(image.File, containerName);
            }

            return NoContent();
        }
    }
}
