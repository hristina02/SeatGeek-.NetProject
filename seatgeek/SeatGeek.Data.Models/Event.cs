namespace SeatGeek.Data.Models
{
   
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Event;

    public class Event
    {

        public Event()
        {
            this.Tickets=new List<Ticket>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; } = null!;

        [Required]
        [MaxLength(CityMaxLength)]
        public string City { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(MaxCapacityConst)]
        public int MaxCapacity {  get; set; } 

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        public virtual ICollection<Ticket> Tickets { get; set; } 

        public DateTime CreatedOn { get; set; }
        
        public bool IsActive { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;


        public Guid AgentId { get; set; }

        public virtual Agent Agent { get; set; } = null!;

       

    }
}
