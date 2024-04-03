
namespace SeatGeek.Web.ViewModels.Order
{
    using SeatGeek.Web.ViewModels.Ticket;
    using static SeatGeek.Common.EntityValidationConstants.Order;
    public class OrderDetailsViewModel
    {

        public int OrderId { get; set; }

      
        public int EventID { get; set; }

       
        public DateTime OrderDate { get; set; }


        public string Title { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string Start { get; set; } = null!;

        public string End { get; set; } = null!;
        public int NumberTickets { get; set; }

        public decimal OrderTotal { get; set; }
        public List<TicketFormModel> Tickets { get; set; } = null!;

        
    }
}
