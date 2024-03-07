
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SeatGeek.Data;
using SeatGeek.Data.Models;
using SeatGeek.Data.Models.Enums;
using SeatGeek.Services.Data.Interfaces;
using SeatGeek.Web.ViewModels.Event;
using SeatGeek.Web.ViewModels.Order;
using SeatGeek.Web.ViewModels.Ticket;
namespace SeatGeek.Services.Data
{
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
                OrderId = orderModel.OrderID,
                EventID = orderModel.EventID,
                UserId = Guid.Parse(userId),
                OrderDate = orderModel.OrderDate,
                NumberTickets = orderModel.NumberTickets,
                OrderTotal = orderModel.OrderTotal
            };





            await this.dbContext.Orders.AddAsync(order);
            await this.dbContext.SaveChangesAsync();
            return orderModel.OrderID.ToString();


        }
    }
}
