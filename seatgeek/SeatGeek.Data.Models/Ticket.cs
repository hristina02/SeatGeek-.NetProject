namespace SeatGeek.Data.Models
{
    using SeatGeek.Data.Models.Enums;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Common.EntityValidationConstants.Ticket;

    public class Ticket
    {
        public Ticket()
        {
            TicketOwners = new HashSet<ApplicationUser>();
        }


        [Key]
        public int Id { get; set; }
       
        public Guid ApplicationUser{ get; set; }
        
        public virtual ICollection<ApplicationUser> TicketOwners { get; set; } = null!;

        [Required]
        public int EventId { get; set; }
      
        [ForeignKey(nameof(EventId))]

        public virtual Event Event { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

   
       public TicketTypeEnum Type { get; set; }

        [Required]
        [MaxLength(MaxQuantity)]
        public int Quantity { get; set; }

        
        
    }
}
