namespace SeatGeek.Web.Controllers
{

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SeatGeek.Services.Data.Interfaces;
    using SeatGeek.Services.Data.Model.Event;
    using SeatGeek.Web.Infrastructure.Extensions;
    using SeatGeek.Web.ViewModels.Category;
    using SeatGeek.Web.ViewModels.Event;
    using SeatGeek.Web.ViewModels.Ticket;
    using static Common.NotificationMessagesConstants;
    
    [Authorize]
    public class TicketController : Controller
    {
        private readonly IAgentService agentService;
        private readonly IEventService eventService;
        private readonly ICategoryService categoryService;
        public TicketController(ICategoryService categoryService, IAgentService agentService,
            IEventService eventService)
        {
            this.categoryService = categoryService;
            this.agentService = agentService;
            this.eventService = eventService;
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            

            try
            {
                TicketFormModel formModel = new TicketFormModel();
               

                return View(formModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }


        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later or contact administrator";

            return this.RedirectToAction("Index", "Home");
        }
    }

   
}
