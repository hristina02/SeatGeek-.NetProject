namespace SeatGeek.Services.Tests
{
   
    using Microsoft.EntityFrameworkCore;
    using Data;
    using Data.Interfaces;
   
    using static DatabaseSeeder;
    using SeatGeek.Data;
    using NUnit.Framework;

    public class AgentServiceTests
    {
        // First way: Using InMemory Database
        // Pros: Testing is as close to the production scenario as possible
        // Cons: You are testing EFCore functionality as well, so this is not good UNIT test
        // Hard to arrange the scenario
        // Second way: Using Mock of IRepository
        // Pros: Good unit testing, tests single unit, easy push test data
        // Cons: You need to have repository pattern
        private DbContextOptions<SeatGeekDbContext> dbOptions;
        private SeatGeekDbContext dbContext;

        private IAgentService agentService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<SeatGeekDbContext>()
                .UseInMemoryDatabase("SeatGeekInMemory" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new SeatGeekDbContext(this.dbOptions, false);

            this.dbContext.Database.EnsureCreated();
            SeedDatabase(this.dbContext);

            this.agentService = new AgentService(this.dbContext);
        }

        [Test]
        public async Task AgentExistsByUserIdAsyncShouldReturnTrueWhenExists()
        {
            string existingAgentUserId = AgentUser.Id.ToString();

            bool result = await this.agentService.AgentExistsByUserIdAsync(existingAgentUserId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task AgentExistsByUserIdAsyncShouldReturnFalseWhenNotExists()
        {
            string existingAgentUserId = User.Id.ToString();

            bool result = await this.agentService.AgentExistsByUserIdAsync(existingAgentUserId);

            Assert.IsFalse(result);
        }

        //[Test]
        //public async Task AgentExistsByUserIdAsyncShouldReturnFalseWhenNotExists()
        //{
        //    string existingAgentUserId = User.Id.ToString();

        //    bool result = await this.agentService.AgentExistsByUserIdAsync(existingAgentUserId);

        //    Assert.IsFalse(result);
        //}
    }
}