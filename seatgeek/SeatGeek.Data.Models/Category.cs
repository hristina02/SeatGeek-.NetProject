namespace SeatGeek.Data.Models   
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Category;
    public class Category
    {
        public Category()
        {
            Events = new HashSet<Event>();
        }
       
        
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public bool IsActive { get; set; }
        public virtual ICollection<Event> Events { get; set; } = null!;
    }
}
