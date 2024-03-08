namespace SeatGeek.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SeatGeek.Data.Models.Enums;
    using SeatGeek.Services.Data.Interfaces;
    using SeatGeek.Services.Data.Model.Event;
    using SeatGeek.Web.Infrastructure.Extensions;
    using SeatGeek.Web.ViewModels.Category;
    using SeatGeek.Web.ViewModels.Event;
    using SeatGeek.Web.ViewModels.Ticket;
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllEventsQueryModel queryModel)
        {
            AllEventsFilteredAndPagedServiceModel serviceModel =
                await this.eventService.AllAsync(queryModel);

            queryModel.Events = serviceModel.Events;
            queryModel.TotalEvents = serviceModel.TotalEventsCount;
            queryModel.Categories = await this.categoryService.AllCategoryNamesAsync();

            return this.View(queryModel);
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
                    Tickets = new List<TicketFormModel>()
                    //Tickets = new List<TicketFormModel>
                    // {
                    //     new TicketFormModel { Type = "Gold" },
                    //     new TicketFormModel { Type = "Silver" },
                    //     new TicketFormModel { Type = "Bronze" }
                    //     // Добавете други видове билети, ако е необходимо
                    // }
                };

                foreach (TicketTypeEnum type in Enum.GetValues(typeof(TicketTypeEnum)))
                {
                    formModel.Tickets.Add(new TicketFormModel { Type = type.ToString() });
                }

                return View(formModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
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

            int totalTicketQuantity = model.Tickets.Sum(ticket => ticket.Quantity);
            if (totalTicketQuantity >model.MaxCapacity) // Use MaxCapacity for comparison
            {
                this.TempData[ErrorMessage] = "Yout Tickets are more than the evenr quantity!";
                model.Categories = await this.categoryService.AllCategoriesAsync();
                return this.View(model);
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            EventDetailsViewModel? viewModel = await this.eventService
                .GetDetailsByIdAsync(id);
            if (viewModel==null)
            {
                this.TempData[ErrorMessage] = "Event with the provided id does not exist!";

                return this.RedirectToAction("All", "Event");
            }

            
                    //EventDetailsViewModel viewModel = await this.eventService
                    //.GetDetailsByIdAsync(id);

                return View(viewModel);
           
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool eventExists = await this.eventService
                .ExistsByIdAsync(id);
            if (!eventExists)
            {
                this.TempData[ErrorMessage] = "Event with the provided id does not exist!";

                return this.RedirectToAction("All", "Event");
            }

            bool isUserAgent = await this.agentService
                .AgentExistsByUserIdAsync(this.User.GetId()!);
            if (!isUserAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit event info!";

                return this.RedirectToAction("Become", "Agent");
            }

            string? agentId =
                await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);
            bool isAgentOwner = await this.eventService
                .IsAgentWithIdOwnerOfEventWithIdAsync(id, agentId!);
            if (!isAgentOwner)
            {
                this.TempData[ErrorMessage] = "You must be the agent owner of the Event you want to edit!";

                return this.RedirectToAction("Mine", "Event");
            }

            try
            {

                EventFormModel formModel = await this.eventService
                .GetEventForEditByIdAsync(id);
                formModel.Categories = await this.categoryService.AllCategoriesAsync();

                return this.View(formModel);
            }
            catch(Exception ) 
            {
                return this.GeneralError();            
            
            } 
            
            
        }


        [HttpPost]
        public async Task<IActionResult> Edit(string id, EventFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Categories = await this.categoryService.AllCategoriesAsync();

                return this.View(model);
            }

            bool eventExists = await this.eventService
                .ExistsByIdAsync(id);
            if (!eventExists)
            {
                this.TempData[ErrorMessage] = "Even with the provided id does not exist!";

                return this.RedirectToAction("All", "Event");
            }

            bool isUserAgent = await this.agentService
                .AgentExistsByUserIdAsync(this.User.GetId()!);
            if (!isUserAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit Event info!";

                return this.RedirectToAction("Become", "Agent");
            }

            string? agentId =
                await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);
            bool isAgentOwner = await this.eventService
                .IsAgentWithIdOwnerOfEventWithIdAsync(id, agentId!);
            if (!isAgentOwner)
            {
                this.TempData[ErrorMessage] = "You must be the agent owner of the event you want to edit!";

                return this.RedirectToAction("Mine", "Event");
            }

            try
            {
                await this.eventService.EditEventByIdAndFormModelAsync(id, model);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty,
                    "Unexpected error occurred while trying to update the event. Please try again later or contact administrator!");
                model.Categories = await this.categoryService.AllCategoriesAsync();

                return this.View(model);
            }

            this.TempData[SuccessMessage] = "Event was edited successfully!";
            return this.RedirectToAction("Details", "Event", new { id });
        }



        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<EventAllViewModel> myEvents =
                new List<EventAllViewModel>();

            string userId = this.User.GetId()!;
            bool isUserAgent = await this.agentService
                .AgentExistsByUserIdAsync(userId);


            try
            {
                if (isUserAgent)
                {
                    string? agentId =
                        await this.agentService.GetAgentIdByUserIdAsync(userId);

                    myEvents.AddRange(await this.eventService.AllByAgentIdAsync(agentId!));
                }
                else
                {
                    myEvents.AddRange(await this.eventService.AllByUserIdAsync(userId));
                }





                return this.View(myEvents);

            }
            catch(Exception)
            {
              return this.GeneralError();

            }



        }


        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            bool houseExists = await this.eventService
                .ExistsByIdAsync(id);
            if (!houseExists)
            {
                this.TempData[ErrorMessage] = "Evant with the provided id does not exist!";

                return this.RedirectToAction("All", "Event");
            }

            bool isUserAgent = await this.agentService
                .AgentExistsByUserIdAsync(this.User.GetId()!);
            if (!isUserAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit event info!";

                return this.RedirectToAction("Become", "Agent");
            }

            string? agentId =
                await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);
            bool isAgentOwner = await this.eventService
                .IsAgentWithIdOwnerOfEventWithIdAsync(id, agentId!);
            if (!isAgentOwner)
            {
                this.TempData[ErrorMessage] = "You must be the agent owner of the house you want to edit!";

                return this.RedirectToAction("Mine", "Event");
            }

            try
            {
                EventPreDeleteDetailsViewModel viewModel =
                    await this.eventService.GetEventForDeleteByIdAsync(id);

                return this.View(viewModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id, EventPreDeleteDetailsViewModel model)
        {
            bool eventExists = await this.eventService
                .ExistsByIdAsync(id);
            if (!eventExists)
            {
                this.TempData[ErrorMessage] = "Event with the provided id does not exist!";

                return this.RedirectToAction("All", "Event");
            }

            bool isUserAgent = await this.agentService
                .AgentExistsByUserIdAsync(this.User.GetId()!);
            if (!isUserAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit event info!";

                return this.RedirectToAction("Become", "Agent");
            }

            string? agentId =
                await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);
            bool isAgentOwner = await this.eventService
                .IsAgentWithIdOwnerOfEventWithIdAsync(id, agentId!);
            if (!isAgentOwner)
            {
                this.TempData[ErrorMessage] = "You must be the agent owner of the event you want to edit!";

                return this.RedirectToAction("Mine", "Event");
            }

            try
            {
                await this.eventService.DeleteEventByIdAsync(id);

                this.TempData[WarningMessage] = "The event was successfully deleted!";
                return this.RedirectToAction("Mine", "Event");
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
