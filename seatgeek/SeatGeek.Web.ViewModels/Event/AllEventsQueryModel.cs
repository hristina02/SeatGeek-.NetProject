namespace SeatGeek.Web.ViewModels.Event
{

    using System.ComponentModel.DataAnnotations;
    using Enums;
    using static Common.GeneralValidationConstants;
    public class AllEventsQueryModel
    {
        public AllEventsQueryModel()
        {
            this.CurrentPage = DefaultPage;
            this.EventsPerPage = EntitiesPerPage;

            this.Categories = new HashSet<string>();
            this.Events = new HashSet<EventAllViewModel>();
        }

        public string? Category { get; set; }

        [Display(Name = "Search by word")]
        public string? SearchString { get; set; }

        [Display(Name = "Sort Events By")]
        public EventSorting EventSorting { get; set; }

        public int CurrentPage { get; set; }

        [Display(Name = "Show Events On Page")]
        public int EventsPerPage { get; set; }

        public int TotalEvents { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<EventAllViewModel> Events { get; set; }
    }
}
