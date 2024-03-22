namespace SeatGeek.Web.ViewModels.Event
{
    using SeatGeek.Web.ViewModels.Agent;
    using SeatGeek.Web.ViewModels.Ticket;

    public class EventDetailsViewModel:EventAllViewModel
    {
        public string Description { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string Start {  get; set; } 


        public string End { get; set; }

        public string Address { get; set; }

        public List<TicketFormModel> TicketList { get; set; } = new List<TicketFormModel>();

        public AgentInfoOnHouseViewModel Agent { get; set; } = null!;
    }
}
