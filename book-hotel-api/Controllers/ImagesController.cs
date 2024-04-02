using AutoMapper;
using book_hotel_api.DTOs;
using book_hotel_api.Entities;
using book_hotel_api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace book_hotel_api.Controllers
{
    [ApiController]
    [Route("api/images")]
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
        public async Task<ActionResult<List<ImageDTO>>> Get(int hotelId)
        {
            var queryable = _context.Image.AsQueryable().Where(x => x.HotelId == hotelId);
            var images = await queryable.OrderBy(x => x.Id).ToListAsync();
            return _mapper.Map<List<ImageDTO>>(images);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Post([FromForm] List<ImageCreationDTO> imageCreationDTOs)
        {
            if (!imageCreationDTOs.IsNullOrEmpty())
            {
                foreach (var imageCreationDTO in imageCreationDTOs)
                {
                    var image = _mapper.Map<Image>(imageCreationDTO);

                    image.File = await _fileStorageService.SaveFile(containerName, imageCreationDTO.File);

                    _context.Add(image);
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
