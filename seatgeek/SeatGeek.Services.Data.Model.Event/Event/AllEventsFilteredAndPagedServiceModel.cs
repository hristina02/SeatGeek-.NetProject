namespace SeatGeek.Services.Data.Models.Event
{
    using SeatGeek.Web.ViewModels.Event;
    public class AllEventsFilteredAndPagedServiceModel
    {
        
            public AllEventsFilteredAndPagedServiceModel()
            {
                this.Events = new HashSet<EventAllViewModel>();
            }

            public int TotalEventsCount { get; set; }

            public IEnumerable<EventAllViewModel>Events { get; set; }
        
    }
}
