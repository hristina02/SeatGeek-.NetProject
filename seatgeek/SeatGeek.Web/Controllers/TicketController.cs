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
    using SeatGeek.Web.ViewModels.Order;
    using SeatGeek.Web.ViewModels.Ticket;
    using static Common.NotificationMessagesConstants;
    
    [Authorize]
    public class TicketController : Controller
    {
        private readonly IAgentService agentService;
        private readonly IEventService eventService;
        private readonly ICategoryService categoryService;
        private readonly ITicketService ticketService;
        public TicketController(ICategoryService categoryService, IAgentService agentService,
            IEventService eventService, ITicketService ticketService)
        {
            this.categoryService = categoryService;
            this.agentService = agentService;
            this.eventService = eventService;
            this.ticketService = ticketService;
        }


        [HttpGet]
        public async Task<IActionResult> Add(string id)
        {


             try
            {
                // Assuming id is the eventId and it's not null or empty
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest("Invalid eventId");
                }

                IEnumerable<TicketFormModel> formModels = await ticketService.GetTicketsAsync(id);

                // Retrieve ticket details based on the eventId

                if (formModels == null || !formModels.Any())
                {
                    // Handle the case where the ticket details are not found
                    return NotFound("Ticket not found");
                }

                return View(formModels.ToList()); // Convert to List<TicketFormModel> if needed
            }
            catch (Exception)
            {
                // Log the exception or handle it as needed
                return this.GeneralError();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Add(IEnumerable<TicketFormModel> ticketModels,string id)
        {
            try
            {
                // Validate the ticketModels
                if (ticketModels == null || !ticketModels.Any())
                {
                    // Handle the case where no ticket models are provided
                    return BadRequest("No ticket models provided");
                }

                // Convert each TicketFormModel to OrderViewModel
                OrderViewModel orderViewModels =new OrderViewModel()
                {
                   EventID=id,
                   OrderDate = DateTime.Now,

                }).ToList();

                // Call the service method to create the orders
                string result = await ticketService.CreateOrdersAsync(orderViewModels);

                if (result == "Orders created successfully")
                {
                    // Redirect to a success page or another action after handling the purchase
                    return RedirectToAction("PurchaseSuccess");
                }
                else
                {
                    // Handle the case where order creation failed
                    return View("ErrorView"); // Replace "ErrorView" with your error view name or logic
                }
            }
            catch (Exception)
            {
                // Log the exception or handle it as needed
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
