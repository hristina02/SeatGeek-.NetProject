using Microsoft.AspNetCore.Mvc;
using SeatGeek.Services.Data.Interfaces;

namespace SeatGeek.Web.Controllers
{
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
    }
}
