using Microsoft.AspNetCore.Mvc;

namespace SeatGeek.Web.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
