namespace SeatGeek.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    public class CategoryEvent
    {
        public int ChildCategoryId { get; set; }
       
        [ForeignKey(nameof(ChildCategoryId))]
        public Category ChildCategory { get; set; } = null!;

        public int EventId { get; set; }

        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; }=null!;
    }
}
