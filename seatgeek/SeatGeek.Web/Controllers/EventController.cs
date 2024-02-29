namespace SeatGeek.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SeatGeek.Services.Data.Interfaces;
    using SeatGeek.Web.Infrastructure.Extensions;
    using SeatGeek.Web.ViewModels.Category;
    using SeatGeek.Web.ViewModels.Event;
    using static Common.NotificationMessagesConstants;
    [Authorize]
    public class EventController : Controller
    {
        private readonly IAgentService agentService;
        private readonly IEventService eventService;
        private readonly ICategoryService categoryService;
        public EventController(ICategoryService categoryService, IAgentService agentService,
            IEventService eventService)
        {
            this.categoryService = categoryService;
            this.agentService = agentService;
            this.eventService = eventService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            bool isAgent =
                await this.agentService.AgentExistsByUserIdAsync(this.User.GetId()!);
            if (!isAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to add new event!";

                return this.RedirectToAction("Become", "Agent");
            }

            try
            {
                EventFormModel formModel = new EventFormModel()
                {

                    Categories = await this.categoryService.AllCategoriesAsync(),
                     Tickets = new List<TicketFormModel>
                     {
                         new TicketFormModel { Type = "Gold" },
                         new TicketFormModel { Type = "Silver" },
                         new TicketFormModel { Type = "Bronze" }
                         // Добавете други видове билети, ако е необходимо
                     }
                };

                return View(formModel);
            }
            catch (Exception)
            {
                return this.View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(EventFormModel model)
        {
            bool isAgent =
                await this.agentService.AgentExistsByUserIdAsync(this.User.GetId()!);
            if (!isAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to add new event!";

                return this.RedirectToAction("Become", "Agent");
            }

            bool categoryExists =
                await this.categoryService.ExistsByIdAsync(model.CategoryId);
            if (!categoryExists)
            {
                // Adding model error to ModelState automatically makes ModelState Invalid
                this.ModelState.AddModelError(nameof(model.CategoryId), "Selected category does not exist!");
            }

            if (!this.ModelState.IsValid)
            {
                model.Categories = await this.categoryService.AllCategoriesAsync();

                return this.View(model);
            }

            try
            {
                string? agentId =
                    await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);

                string eventId =
                    await this.eventService.CreateAndReturnIdAsync(model, agentId!);

                this.TempData[SuccessMessage] = "Event was added successfully!";
                return this.RedirectToAction("Details", "Event", new { id = eventId });
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new event! Please try again later or contact administrator!");
                model.Categories = await this.categoryService.AllCategoriesAsync();

                return this.View(model);
            }
        }

    }
}
