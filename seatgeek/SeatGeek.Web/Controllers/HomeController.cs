using Microsoft.AspNetCore.Mvc;
using SeatGeek.Web.ViewModels;
using System.Diagnostics;

namespace SeatGeek.Web.Controllers
{
    public class HomeController : Controller
    {
       
        public HomeController()
        {
           
        }

        public IActionResult Index()
        {
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
