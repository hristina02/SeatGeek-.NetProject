namespace SeatGeek.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    public class CategoryEvent
    {
        public int CategoryId { get; set; }
       
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        public int EventId { get; set; }

        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; }=null!;
    }
}
