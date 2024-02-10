﻿namespace SeatGeekProject.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;
    using SeatGeek.Data.Models;

    public class SeatGeekDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public SeatGeekDbContext(DbContextOptions<SeatGeekDbContext> options)
           : base(options)
        {

        }

        public DbSet<ChildCategory> ChildCategories{ get; set; } = null!;

        public DbSet<ParentCategory> ParentCategories { get; set; }=null!;

        public DbSet<Ticket> Tickets { get; set; } = null!;

        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<CategoryEvent> CategoryEvents { get; set; } = null!;

        public DbSet<Agent> Agents { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly configAssembly = Assembly.GetAssembly(typeof(SeatGeekDbContext)) ??
                                      Assembly.GetExecutingAssembly();
            builder.ApplyConfigurationsFromAssembly(configAssembly);

            base.OnModelCreating(builder);
        }
    }
}
