using Microsoft.AspNetCore.Mvc;
using SeatGeek.Services.Data.Interfaces;
using SeatGeek.Web.Areas.Admin.ViewModels.Event;
using SeatGeek.Web.Infrastructure.Extensions;

namespace SeatGeek.Web.Areas.Admin.Controllers
{
    public class EventController : BaseAdminController
    {
        private readonly IAgentService agentService;
        private readonly IEventService  eventService;

        public EventController(IAgentService agentService, IEventService eventService)
        {
            this.agentService = agentService;
            this.eventService = eventService;
        }

        public async Task<IActionResult> Mine()
        {
            string? agentId =
                await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);
            MyEventsViewModel viewModel = new MyEventsViewModel()
            {
                AddedEvents = await this.eventService.AllByAgentIdAsync(agentId!),
               
            };

            return this.View(viewModel);
        }
    }
}
