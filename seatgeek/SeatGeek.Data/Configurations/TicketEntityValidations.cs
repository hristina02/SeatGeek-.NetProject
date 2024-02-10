namespace SeatGeek.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SeatGeek.Data.Models;
    using SeatGeek.Data.Models.Enums;
    public class TicketEntityValidations : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasData(this.GenerateTicket());
        }

        private Ticket[] GenerateTicket()
        {
            ICollection<Ticket> tickets = new HashSet<Ticket>();

            Ticket ticket;

            ticket = new Ticket()
            {
                Id = 1,
                EventId = Guid.Parse("24A75A75 - BA22 - 463B - B8F0 - C29D62C8FA73"),
                Quantity = 1,
                Price = 250,
                Type = TicketTypeEnum.Silver,
                //TicketOwners = new[] { Guid.Parse("3D4376DC-52F7-443C-9303-247A002B680D")}
               



            };
            tickets.Add(ticket);






            return tickets.ToArray();
        }

    }
}
