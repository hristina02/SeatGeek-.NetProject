using Microsoft.AspNetCore.Mvc;

namespace SeatGeek.Web.Controllers
{
    public class TicketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
