namespace SeatGeek.Services.Tests
{
    using SeatGeek.Data;
    using SeatGeek.Data.Models;

    public static class DatabaseSeeder
    {
        public static ApplicationUser AgentUser;
        public static ApplicationUser User;
        public static Agent Agent;
        public static Event Event;

        public static void SeedDatabase(SeatGeekDbContext dbContext)
        {
            AgentUser = new ApplicationUser()
            {
                UserName = "Mariq",
                NormalizedUserName = "MARIQ",
                Email = "mp@agents.com",
                NormalizedEmail = "MP@AGENTS.COM",
                EmailConfirmed = true,
                PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                ConcurrencyStamp = "caf271d7-0ba7-4ab1-8d8d-6d0e3711c27d",
                SecurityStamp = "ca32c787-626e-4234-a4e4-8c94d85a2b1c",
                TwoFactorEnabled = false,
                FirstName = "Mariq",
                LastName = "Petrova"
            };
            User = new ApplicationUser()
            {
                UserName = "Gosho",
                NormalizedUserName = "GOSHO",
                Email = "gosho@users.com",
                NormalizedEmail = "GOSHO@S.COM",
                EmailConfirmed = true,
                PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                ConcurrencyStamp = "8b51706e-f6e8-4dae-b240-54f856fb3004",
                SecurityStamp = "f6af46f5-74ba-43dc-927b-ad83497d0387",
                TwoFactorEnabled = false,
                FirstName = "Gosho",
                LastName = "Goshov"
            };
            Agent = new Agent()
            {
                PhoneNumber = "+359785888888",
                User = AgentUser
            };
            Event = new Event()
            {

                Id = 5,
                Title = "Dara ",
                Address = "North London",
                City = "London",
                Description = "Dara  ushers in 2024. with a concept show event on Valentine's Day. Spend February 14 at Bar Petak with the pop girl of the Bulgarian scene and your favorite songs of hers.",
                MaxCapacity = 150,
                ImageUrl = "https://bg.content.eventim.com/static/uploaded/bg/3/v/9/g/3v9g_300_300.jpeg",
                AgentId = Guid.Parse("4BB6EE6B-0068-4112-91D5-475706808740"),
                CategoryId = 1,
                IsActive = true

            };

            dbContext.Events.Add(Event);
            dbContext.Users.Add(AgentUser);
            dbContext.Users.Add(User);
            dbContext.Agents.Add(Agent);

            dbContext.SaveChanges();
        }
    }
    
}
