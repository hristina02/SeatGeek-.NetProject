namespace SeatGeek.Web.ViewModels.Event
{
    public class EventAllViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public string City { get; set; } = null!;
    
        public string Description { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;


    }
}
