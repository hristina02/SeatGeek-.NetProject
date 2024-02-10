namespace SeatGeek.Data.Models
{ 
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Common.EntityValidationConstants.Agent;

    /// <summary>Agent create Events</summary>
    public class Agent
    {

        public Agent()
        {
            this.Id = Guid.NewGuid();
            this.OwnedEvents= new HashSet<Event>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        public Guid UserId { get; set; }
        
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        public virtual ICollection<Event> OwnedEvents{ get; set; }
    }
}
