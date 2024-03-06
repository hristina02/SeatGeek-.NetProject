
using Microsoft.EntityFrameworkCore;
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


        public async Task<OrderViewModel> CreateOrderAndReturnIdAsync(TicketFormModel formModel, string agentId)
        {
            // Create the Event entity
            OrderViewModel orderModel = new Event
            {

                Title = formModel.Title,
                Address = formModel.Address,
                Description = formModel.Description,
                City = formModel.City,
                ImageUrl = formModel.ImageUrl,
                MaxCapacity = formModel.MaxCapacity,
                CategoryId = formModel.CategoryId,
                IsActive = true,
                AgentId = Guid.Parse(agentId)
            };


            // Create and add tickets to the event
            foreach (var ticketModel in formModel.Tickets)
            {
                TicketTypeEnum ticketType;
                if (Enum.TryParse(ticketModel.Type, out ticketType))
                {
                    Ticket ticket = new Ticket
                    {
                        Type = ticketType,
                        Quantity = ticketModel.Quantity,
                        Price = ticketModel.Price,
                    };

                    eventModel.Tickets.Add(ticket);
                }
            }


            await this.dbContext.Events.AddAsync(eventModel);
            await this.dbContext.SaveChangesAsync();
            return eventModel.Id.ToString();





        }
    }
}
