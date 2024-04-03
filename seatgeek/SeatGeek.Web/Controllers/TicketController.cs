namespace SeatGeek.Web.Controllers
{

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SeatGeek.Data.Models.Enums;
    using SeatGeek.Services.Data.Interfaces;
    using SeatGeek.Services.Data.Models.Event;
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


        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<MineOrdersViewModel> myOrderedTickets =
                new List<MineOrdersViewModel>();



            try
            {
                string userId = this.User.GetId()!;


                myOrderedTickets.AddRange(await this.ticketService.AllOrderedTicketsByUserIdAsync(userId!));



                return View(myOrderedTickets);
            }
            catch (Exception)
            {
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
                this.TempData[ErrorMessage] = "Event with the provided id does not exist!";

                return this.RedirectToAction("All", "Event");
            }

            
            var userId = this.User.GetId();

            try
            {
                
                if (orderFormModel == null || orderFormModel.Tickets == null || orderFormModel.Tickets.Count == 0)
                {
                    return BadRequest("No tickets provided");
                }

                // Assuming EventId is already a string
                var eventId = orderFormModel.EventID;

                decimal totalSum = 0;
                int num = 0;
                //OrderViewModel orderViewModel = new OrderViewModel();

                foreach (var ticketModel in orderFormModel.Tickets)
                {
                    if (ticketModel.NumberForEveryModel > 0)
                    {
                        totalSum += ticketModel.Price * ticketModel.NumberForEveryModel;

                        num += ticketModel.NumberForEveryModel;
                        if (num < ticketModel.NumberForEveryModel)
                        {
                            this.TempData[ErrorMessage] = "Event with the provided id does not exist!";

                            return this.RedirectToAction("All", "Event");
                        }
                    }
                 
                }

                var start=eventService.GetDetailsByIdAsync(userId);
                
                OrderFormModel orderViewModel = new OrderFormModel()
                {
                    OrderId = orderFormModel.OrderId,
                    EventID = int.Parse(id),
                    OrderDate = DateTime.Now,
                    NumberTickets=num,
                    OrderTotal = totalSum
                   
                    // Include other properties from TicketFormModel as needed
                };

                // Call the service method to create the orders
                string orderId = await ticketService.CreateOrderIdAsync(orderViewModel, userId);


                this.TempData[SuccessMessage] = "You Add tickets succesfully!";

                return this.RedirectToAction("Details", "Ticket", new {Id=orderId});

            }
            catch (Exception)
            {
                // Log the exception or handle it as needed
                return this.GeneralError();
            }
        }

        [HttpGet]
        
        public async Task<IActionResult> Details(string Id)
        {
            OrderDetailsViewModel viewModel = await this.ticketService
                .GetDetailsByIdAsync(Id);
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
