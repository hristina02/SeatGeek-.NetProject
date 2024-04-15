namespace SeatGeek.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;
    using SeatGeek.Data.Models;
    using SeatGeek.Data.Configurations;
    using System.Reflection.Emit;

    public class SeatGeekDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        private readonly bool seedDb;
        public SeatGeekDbContext(DbContextOptions<SeatGeekDbContext> options, bool seedDb=true)
           : base(options)
        {
            this.seedDb = seedDb;
        }

        public DbSet<Category> Categories{ get; set; } = null!;

        public DbSet<Ticket> Tickets { get; set; } = null!;

        public DbSet<Event> Events { get; set; } = null!;
     
        public DbSet<CategoryEvent> CategoryEvents { get; set; } = null!;

        public DbSet<Agent> Agents { get; set; } = null!;


        public DbSet<Order> Orders { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CategoryEvent>().HasNoKey();
            // Apply entity configurations
            builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
            builder.ApplyConfiguration(new SeedEventEntityConfiguration());

            // Apply seed data configurations (if applicable)
            if (seedDb)
            {
                builder.ApplyConfiguration(new CategoryEntityConfiguration());
                
                builder.ApplyConfiguration(new TicketEntityConfiguration());
                // Add more seed data configurations if needed
            }
            base.OnModelCreating(builder);

        }
    }
}
