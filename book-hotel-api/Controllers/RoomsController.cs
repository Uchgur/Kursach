using Microsoft.AspNetCore.Mvc;

namespace book_hotel_api.Controllers
{
    public class RoomsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
