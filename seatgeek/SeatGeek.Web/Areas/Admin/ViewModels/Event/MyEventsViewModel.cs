namespace SeatGeek.Web.Areas.Admin.ViewModels.Event
{
    using SeatGeek.Web.ViewModels.Event;
    public class MyEventsViewModel
    {
        public IEnumerable<EventAllViewModel> AddedEvents { get; set; } = null!;

        //public IEnumerable<EventAllViewModel> Buye{ get; set; } = null!;


    }
}
