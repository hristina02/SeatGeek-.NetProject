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

                IEnumerable<TicketFormModel> ticketFormModels = await ticketService.GetTicketsAsync(id);

                OrderFormModel orderFormModel = new OrderFormModel()
                { 
                    EventID=int.Parse(id),
                    Tickets = ticketFormModels.ToList()
                };
                // Retrieve ticket details based on the eventId

                if (orderFormModel == null)
                {
                    // Handle the case where the ticket details are not found
                    return NotFound("Ticket not found");
                }

                return View(orderFormModel); // Convert to List<TicketFormModel> if needed
             }
             catch (Exception)
             {
                // Log the exception or handle it as needed
                return this.GeneralError();
             }

        }

        [HttpPost]
        public async Task<IActionResult> Add(OrderFormModel orderFormModel,string id)
        {


            bool eventExists = await this.eventService
               .ExistsByIdAsync(id);
            if (!eventExists)
            {
                this.TempData[ErrorMessage] = "Even with the provided id does not exist!";

                return this.RedirectToAction("All", "Event");
            }


            var userId = this.User.GetId();

            try
            {
                // Validate the ticketModels
                if (orderFormModel == null )
                {
                    return BadRequest("No ticket models provided");
                }

                // Assuming EventId is already a string
                var eventId = orderFormModel.EventID;

                decimal totalSum = 0;
                int num = 0;
                //OrderViewModel orderViewModel = new OrderViewModel();

                foreach (var ticketModel in orderFormModel.Tickets)
                {
                    totalSum += ticketModel.Price * ticketModel.NumberForEveryModel;

                    num += ticketModel.NumberForEveryModel;
                    if(num<ticketModel.Quantity)
                    {
                        this.TempData[ErrorMessage] = "Even with the provided id does not exist!";

                        return this.RedirectToAction("All", "Event");
                    }
                }
                OrderFormModel orderViewModel = new OrderFormModel()
                {
                    EventID = int.Parse(id),
                    OrderDate = DateTime.Now,
                    NumberTickets=num,
                    OrderTotal = totalSum
                    // Include other properties from TicketFormModel as needed
                };

                // Call the service method to create the orders
                string result = await ticketService.CreateOrderIdAsync(orderViewModel, userId);


                this.TempData[SuccessMessage] = "You Add tickets succesfully!";

                return this.RedirectToAction("Details", "Ticket");

            }
            catch (Exception)
            {
                // Log the exception or handle it as needed
                return this.GeneralError();
            }
        }

        [HttpGet]
        
        public async Task<IActionResult> Details( string orderId)
        {
            OrderDetailsViewModel viewModel = await this.ticketService
                .GetDetailsByIdAsync(orderId);
            if (viewModel == null)
            {
                this.TempData[ErrorMessage] = "Order with the provided id does not exist!";

                return this.RedirectToAction("Add", "Ticket");
            }


            

            return View(viewModel);

        }
        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later or contact administrator";

            return this.RedirectToAction("Index", "Home");
        }
    }

   
}
