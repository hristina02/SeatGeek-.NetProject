namespace SeatGeek.Data.Models   
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Category;
    public class ChildCategory
    {
        public ChildCategory()
        {
            Events = new HashSet<Event>();
        }
       
        
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public int ParentCategoryId { get; set; }
        public virtual ParentCategory ParentCategory { get; set; } = null!;

        public virtual ICollection<Event> Events { get; set; } = null!;
    }
}
