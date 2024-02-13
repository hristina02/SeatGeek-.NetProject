using Microsoft.AspNetCore.Mvc;

namespace SeatGeek.Web.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
