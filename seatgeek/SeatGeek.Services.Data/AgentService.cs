namespace SeatGeek.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SeatGeek.Data;
    using SeatGeek.Data.Models;
    using SeatGeek.Services.Data.Interfaces;
    using SeatGeek.Web.ViewModels.Agent;
    using SeatGeek.Web.ViewModels.Event;

    public class AgentService : IAgentService
    {
        private readonly SeatGeekDbContext dbContext;

        public AgentService(SeatGeekDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> AgentExistsByUserIdAsync(string userId)
        {
            bool result = await this.dbContext
                .Agents
                .AnyAsync(a => a.UserId.ToString() == userId);

            return result;
        }

        public async Task<IEnumerable<EventAllViewModel>> AllByAgentIdAsync(string agentId)
        {
            IEnumerable<EventAllViewModel> allAgentEvents = await this.dbContext
                .Events
                .Where(e => e.IsActive &&
                            e.AgentId.ToString() == agentId)
                .Select(e => new EventAllViewModel
                {
                    Id = e.Id,
                    Title = e.Title,
                    City = e.City,
                    ImageUrl = e.ImageUrl,
                   
                })
                .ToArrayAsync();

            return allAgentEvents;
        }

        public async Task<bool> AgentExistsByPhoneNumberAsync(string phoneNumber)
        {
            bool result = await this.dbContext
                .Agents
                .AnyAsync(a => a.PhoneNumber == phoneNumber);

            return result;
        }

        public async Task Create(string userId, BecomeAgentFormModel model)
        {
            Agent newAgent = new Agent()
            {
                PhoneNumber = model.PhoneNumber,
                UserId = Guid.Parse(userId)
            };

            await this.dbContext.Agents.AddAsync(newAgent);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<string?> GetAgentIdByUserIdAsync(string userId)
        {
            Agent? agent = await this.dbContext
                .Agents
                .FirstOrDefaultAsync(a => a.UserId.ToString() == userId);
            if (agent == null)
            {
                return null;
            }

            return agent.Id.ToString();
        }

        public async Task<bool> HasEventWithIdAsync(string? userId, string eventId)
        {
            Agent? agent = await this.dbContext
                .Agents
                .Include(a => a.OwnedEvents)
                .FirstOrDefaultAsync(a => a.UserId.ToString() == userId);
            if (agent == null)
            {
                return false;
            }

            eventId = eventId.ToLower();
            return agent.OwnedEvents.Any(h => h.Id.ToString() == eventId);
        }

    }
}
