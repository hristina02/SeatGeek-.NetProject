namespace SeatGeek.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Category;
    public class ParentCategory
    {
        public ParentCategory()
        {
            ChildCategories = new HashSet<ChildCategory>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<ChildCategory> ChildCategories { get; set; }
    }
}
