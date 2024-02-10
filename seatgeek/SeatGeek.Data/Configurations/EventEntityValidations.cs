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
                .HasDefaultValue(DateTime.UtcNow);

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

        private Event [] GenerateEventModel()
        {
            
            ICollection<Event> events = new HashSet<Event>();

            Event eventModel;

            eventModel = new Event()
            {
                Id=1,
                Title = "Dara Ekimova",
                Address = "North London, UK (near the border)",
                Description = " ",
                ImageUrl = "https://bg.content.eventim.com/static/uploaded/bg/3/v/9/g/3v9g_300_300.jpeg",
                AgentId=Guid.Parse("1F531742-1E18-4D11-8FF6-5E82C8108944"),
                CategoryId=1
                
            };




            events.Add(eventModel);

            return events.ToArray();


        }
    }
}
