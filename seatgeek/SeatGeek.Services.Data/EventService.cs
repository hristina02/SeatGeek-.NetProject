namespace SeatGeek.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SeatGeek.Services.Data.Interfaces;
    using SeatGeek.Web.ViewModels.Home;
    using SeatGeek.Data;
    using SeatGeek.Web.ViewModels.Event;
    using SeatGeek.Data.Models;
    using System.Net.Sockets;
    using SeatGeek.Data.Models.Enums;
    using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

    public class EventService:IEventService
    {
        private readonly SeatGeekDbContext dbContext;

        public EventService(SeatGeekDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<IndexViewModel>> LastFiveEventsAsync()
        {
            IEnumerable<IndexViewModel> lastFiveEvents = await this.dbContext
              .Events
              .OrderByDescending(h => h.CreatedOn)
              .Take(3)
              .Select(h => new IndexViewModel()
              {
                  Id = h.Id.ToString(),
                  Title = h.Title,
                  ImageUrl = h.ImageUrl
              })
              .ToArrayAsync();

            return lastFiveEvents;

        }

        public async Task<string> CreateAndReturnIdAsync(EventFormModel formModel, string agentId)
        {
            // Create the Event entity
            Event eventModel = new Event
            {
                Title = formModel.Title,
                Address = formModel.Address,
                Description = formModel.Description,
                ImageUrl = formModel.ImageUrl,
                MaxCapacity= formModel.MaxCapacity,
                CategoryId = formModel.CategoryId,
                AgentId = Guid.Parse(agentId),
            };

            // Create and add tickets to the event
            foreach (var ticketModel in formModel.Tickets)
            {
                TicketTypeEnum ticketType ;
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

            int totalTicketQuantity = eventModel.Tickets.Sum(ticket => ticket.Quantity);
            if (totalTicketQuantity <= eventModel.MaxCapacity)
            {

                await this.dbContext.Events.AddAsync(eventModel);
                await this.dbContext.SaveChangesAsync();

            }


            return eventModel.Id.ToString();



        }
    }
}
