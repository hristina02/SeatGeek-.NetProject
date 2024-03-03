namespace SeatGeek.Web.ViewModels.Event
{
    using SeatGeek.Web.ViewModels.Agent;

    public class EventDetailsViewModel:EventAllViewModel
    {
        public string Description { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string Address { get; set; }

        public AgentInfoOnHouseViewModel Agent { get; set; } = null!;
    }
}
