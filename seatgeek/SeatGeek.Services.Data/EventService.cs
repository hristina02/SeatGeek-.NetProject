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
    using SeatGeek.Services.Data.Model.Event;
    using SeatGeek.Web.ViewModels.Event.Enums;
    using SeatGeek.Web.ViewModels.Agent;
    using SeatGeek.Web.ViewModels.Ticket;

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
              .Where(h=>h.IsActive)
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
        public async Task<IEnumerable<EventAllViewModel>> AllByUserIdAsync(string userId)
        {

            IEnumerable<EventAllViewModel> allUserEvents = await this.dbContext
            .Events
            .Include(t=>t.Tickets)
            .ThenInclude(u=>u.ApplicationUser)
             .Where(e => e.IsActive &&
                    e.Tickets.Any(t => t.TicketOwners.Any(u => u.Id.ToString() == userId)))
                .Select(h => new EventAllViewModel
                {
                     Id = h.Id,
                     Title = h.Title,
                     City = h.Address,
                    ImageUrl = h.ImageUrl,
                })
               .ToArrayAsync();

            return allUserEvents;
        }


        public async Task<string> CreateAndReturnIdAsync(EventFormModel formModel, string agentId)
        {
            // Create the Event entity
            Event eventModel = new Event
            {
                
                Title = formModel.Title,
                Address = formModel.Address,
                Description = formModel.Description,
                City=formModel.City,
                ImageUrl = formModel.ImageUrl,
                MaxCapacity= formModel.MaxCapacity,
                CategoryId = formModel.CategoryId,
                IsActive=true,
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



        public async Task<EventFormModel> GetEventForEditByIdAsync(string eventId)
        {
            Event eventModel = await this.dbContext
                .Events
                .Include(h => h.Category)
                .Where(h => h.IsActive)
                .FirstAsync(h => h.Id.ToString() == eventId);

            return new EventFormModel
            {
                Title = eventModel.Title,
                Address = eventModel.Address,
                City = eventModel.City,
                Description = eventModel.Description,
                ImageUrl = eventModel.ImageUrl,
                CategoryId = eventModel.CategoryId
            };
        }


        public async Task<bool> ExistsByIdAsync(string eventId)
        {
            bool result = await this.dbContext
                .Events
                .Where(h => h.IsActive)
                .AnyAsync(h => h.Id.ToString() == eventId);

            return result;
        }

        public async Task<EventPreDeleteDetailsViewModel> GetEventForDeleteByIdAsync(string eventId)
        {
            Event eventModel = await this.dbContext
                .Events
                .Where(h => h.IsActive)
                .FirstAsync(h => h.Id.ToString() == eventId);

            return new EventPreDeleteDetailsViewModel
            {
                Title = eventModel.Title,
                Address = eventModel.Address,
                ImageUrl = eventModel.ImageUrl
            };
        }

        public async Task DeleteEventByIdAsync(string eventId)
        {
            Event eventToDelete = await this.dbContext
                .Events
                .Where(h => h.IsActive)
                .FirstAsync(h => h.Id.ToString() == eventId);

            eventToDelete.IsActive = false;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<AllEventsFilteredAndPagedServiceModel> AllAsync(AllEventsQueryModel queryModel)
        {
            IQueryable<Event> eventsQuery = this.dbContext
                .Events
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Category))
            {
                eventsQuery = eventsQuery
                    .Where(h => h.Category.Name == queryModel.Category);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";

                eventsQuery = eventsQuery
                    .Where(h => EF.Functions.Like(h.Title, wildCard) ||
                                EF.Functions.Like(h.City, wildCard) ||
                                EF.Functions.Like(h.Description, wildCard));
            }

            eventsQuery = queryModel.EventSorting switch
            {
                EventSorting.Newest => eventsQuery
                    .OrderByDescending(e => e.CreatedOn),
                EventSorting.Oldest => eventsQuery
                    .OrderBy(e => e.CreatedOn),
               
            };

            IEnumerable<EventAllViewModel>allEvents= await eventsQuery
                .Where(h => h.IsActive)
                .Skip((queryModel.CurrentPage - 1) * queryModel.EventsPerPage)
                .Take(queryModel.EventsPerPage)
                .Select(h => new EventAllViewModel
                {
                    Id = h.Id,
                    Title = h.Title,
                    City = h.City,
                    ImageUrl = h.ImageUrl,
                    
                })
                .ToArrayAsync();
            int totalHouses = eventsQuery.Count();

            return new AllEventsFilteredAndPagedServiceModel()
            {
                TotalEventsCount = totalHouses,
                Events = allEvents
            };
        }

        public async Task<IEnumerable<EventAllViewModel>> AllByAgentIdAsync(string agentId)
        {
            IEnumerable<EventAllViewModel> allAgentEvents = await this.dbContext
               .Events
               .Where(h => h.IsActive &&
                           h.AgentId.ToString() == agentId)
               .Select(h => new EventAllViewModel
               {
                   Id = h.Id,
                   Title = h.Title,
                   City = h.City,
                   ImageUrl = h.ImageUrl,
                  
               })
               .ToArrayAsync();

            return allAgentEvents;
        }

        public async Task<bool> IsAgentWithIdOwnerOfEventWithIdAsync(string eventId, string agentId)
        {
            Event eventModel = await this.dbContext
                .Events
                .Where(h => h.IsActive)
                .FirstAsync(h => h.Id.ToString() == eventId);

            return eventModel.AgentId.ToString() == agentId;
        }


        public async Task<EventDetailsViewModel> GetDetailsByIdAsync(string eventId)
        {
            Event eventModel = await this.dbContext
                .Events
                .Include(e => e.Category)
                .Include(e => e.Tickets)
                .Include(e => e.Agent)
                .ThenInclude(a => a.User)
                .Where(e => e.IsActive)
                .FirstAsync(e => e.Id.ToString() == eventId);

            var ticketModels = eventModel.Tickets.Select(t => new TicketFormModel
            {
                Type = t.Type.ToString(),
                Price = t.Price
                // Add other ticket properties as needed
            }).ToList();

            return new EventDetailsViewModel
            {
                Id = eventModel.Id,
                Title = eventModel.Title,
                Address = eventModel.Address,
                ImageUrl = eventModel.ImageUrl,
                City = eventModel.City,
                Description = eventModel.Description,
                Category = eventModel.Category.Name,
                Agent = new AgentInfoOnHouseViewModel()
                {
                    Email = eventModel.Agent.User.Email,
                    PhoneNumber =eventModel.Agent.PhoneNumber
                },
                TicketList = ticketModels

            };
        }

        public async Task EditEventByIdAndFormModelAsync(string eventId, EventFormModel formModel)
        {
            Event eventModel = await this.dbContext
                 .Events
                 .Where(h => h.IsActive)
                 .FirstAsync(h => h.Id.ToString() == eventId);

            eventModel.Title = formModel.Title;
            eventModel.Address = formModel.Address;
            eventModel.City=formModel.City;
            eventModel.Description = formModel.Description;
            eventModel.ImageUrl = formModel.ImageUrl;
            eventModel.CategoryId = formModel.CategoryId;

            await this.dbContext.SaveChangesAsync();
        }


       
    }
}
