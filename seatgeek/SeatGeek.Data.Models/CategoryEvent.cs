namespace SeatGeek.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    public class CategoryEvent
    {
        public int ChildCategoryId { get; set; }
       
        [ForeignKey(nameof(ChildCategoryId))]
        public ChildCategory ChildCategory { get; set; } = null!;

        public Guid EventId { get; set; }

        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; }=null!;
    }
}
