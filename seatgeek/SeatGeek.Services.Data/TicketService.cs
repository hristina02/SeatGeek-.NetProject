namespace SeatGeek.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SeatGeek.Data;
    using SeatGeek.Data.Models;
    using SeatGeek.Data.Models.Enums;
    using SeatGeek.Services.Data.Interfaces;
    using SeatGeek.Web.ViewModels.Event;
    using SeatGeek.Web.ViewModels.Order;
    using SeatGeek.Web.ViewModels.Ticket;


    public class TicketService:ITicketService
    {

        private readonly SeatGeekDbContext dbContext;

        public TicketService(SeatGeekDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        //public async Task<bool> UserExistsByIdAsync(string userId)
        //{
        //    bool result = await this.dbContext
        //                      .

        //    return result;
        //}

        public async Task<IEnumerable<MineOrdersViewModel>>AllOrderedTicketsByUserIdAsync(string userId)
        {

            IEnumerable<MineOrdersViewModel> allUsersTickets = await dbContext.Orders
               .Where(o => o.UserId.ToString() == userId && o.NumberTickets>0  )
               .Select(o => new MineOrdersViewModel
               {
          
                   Id = o.Id,
                   EventId = o.EventId,
                   OrderDate = o.OrderDate,
                   NumberTickets = o.NumberTickets,
                   OrderTotal = o.OrderTotal,
                   ImageUrl=o.Event.ImageUrl
               })
               .ToArrayAsync();

            return allUsersTickets;

        }
        public async Task<OrderDetailsViewModel> GetDetailsByIdAsync(string orderId)
        {
            Order orderModel = await this.dbContext
                .Orders
                .Include(e => e.Event)
              

                .FirstAsync(e => e.Id.ToString() == orderId);

            return new OrderDetailsViewModel
            {
                OrderId = orderModel.Id,
                Title=orderModel.Event.Title,
                ImageUrl= orderModel.Event.ImageUrl,
                NumberTickets=orderModel.NumberTickets,
                OrderTotal=orderModel.OrderTotal,
                Start=orderModel.Event.Start.ToString(),
                End=orderModel.Event.End.ToString()

                
            };


        }



        public async Task <IEnumerable<TicketFormModel>> GetTicketsAsync(string eventId)
        {
            List<Ticket> ticketModels = await this.dbContext
                .Tickets
                .Where(t => t.EventId.ToString() == eventId).ToListAsync();

            List<TicketFormModel> ticketFormModels = new List<TicketFormModel>();

            foreach (var ticketModel in ticketModels)
            {
                TicketTypeEnum ticketType;
                if (Enum.TryParse(ticketModel.Type.ToString(), out ticketType))
                {
                    TicketFormModel ticket = new TicketFormModel
                    {
                        Type = ticketType.ToString(),
                      
                        Price = ticketModel.Price,
                    };

                    ticketFormModels.Add(ticket);
                }
            }

            return ticketFormModels;
        }




        public async Task<string> CreateOrderIdAsync(OrderFormModel orderModel, string userId)
        {


            Order order = new Order
            {
                Id = orderModel.OrderId,
                EventId = orderModel.EventID,
                UserId = Guid.Parse(userId),
                OrderDate = orderModel.OrderDate,
                NumberTickets = orderModel.NumberTickets,
                OrderTotal = orderModel.OrderTotal
            };





            await this.dbContext.Orders.AddAsync(order);
            await this.dbContext.SaveChangesAsync();
            return order.Id.ToString();


        }
    }
}
