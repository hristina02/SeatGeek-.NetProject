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
                EventId = 1,
                Quantity = 1,
                Price = 250,
                Type = TicketTypeEnum.Silver,




            };






            tickets.Add(ticket);


            return tickets.ToArray();
        }

    }
}
