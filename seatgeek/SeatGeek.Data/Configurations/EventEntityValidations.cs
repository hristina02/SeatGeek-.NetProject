namespace SeatGeek.Data.Configurations
{

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;
   

    public class EventEntityValidations : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder
                .Property(h => h.CreatedOn)
                .HasDefaultValueSql("GETDATE()");
            builder
                .Property(h => h.IsActive)
                .HasDefaultValue(true);
            builder
                .HasOne(h => h.Category)
                .WithMany(c => c.Events)
                .HasForeignKey(h => h.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

           

            builder
                .HasOne(h => h.Agent)
                .WithMany(a => a.OwnedEvents)
                .HasForeignKey(h => h.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(this.GenerateEventModel());
        }

        private Event[] GenerateEventModel()
        {

            ICollection<Event> events = new HashSet<Event>();

            Event eventModel;

            eventModel = new Event()
            {
                Id = 1,
                Title = "Dara Ekimova",
                Address = "North London, UK (near the border)",
                City = "London",
                Description = "Dara Ekimova ushers in 2024. with a concept show event on Valentine's Day. Spend February 14 at Bar Petak with the pop girl of the Bulgarian scene and your favorite songs of hers.",
                MaxCapacity = 100,
                ImageUrl = "https://bg.content.eventim.com/static/uploaded/bg/3/v/9/g/3v9g_300_300.jpeg",
                AgentId = Guid.Parse("4BB6EE6B-0068-4112-91D5-475706808D40"),
               CategoryId = 1,
               IsActive = true
                
            };




            events.Add(eventModel);

            return events.ToArray();


        }
    }
}
