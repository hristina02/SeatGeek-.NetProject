namespace SeatGeek.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SeatGeek.Services.Data.Interfaces;
    using SeatGeek.Web.ViewModels.Home;
    using static Common.GeneralApplicationConstants;
    

    public class HomeController : Controller
    {

        private readonly IEventService eventService;

        public HomeController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public async Task<IActionResult> Index()
        {

            if (this.User.IsInRole(AdminRoleName))
            {
                return this.RedirectToAction("Index", "Home", new { Area = AdminAreaName });
            }

            IEnumerable<IndexViewModel> viewModel =
                await this.eventService.LastFiveEventsAsync();

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400 || statusCode == 404)
            {
                return this.View("Error404");
            }

            if (statusCode == 401)
            {
                return this.View("Error401");
            }

            return this.View();
        }
    }
}
