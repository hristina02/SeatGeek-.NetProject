namespace SeatGeek.Web.ViewModels.Order
{

    using SeatGeek.Data.Models;
    using SeatGeek.Data.Models.Enums;
    using SeatGeek.Web.ViewModels.Ticket;
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Order;
    public class OrderViewModel
    {
        public int OrderID { get; set; }

        //foreign key
        [Required]
        public int EventID { get; set; }


        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [Range(1, OrderMaxNumber, ErrorMessage = "Number must be between 1 and {2}")]
        public int NumberTickets { get; set; }

        [Required]
        public decimal OrderTotal { get; set; }
        IEnumerable<TicketFormModel> Tickets { get; set; }=new List<TicketFormModel>();
        public ApplicationUser? user { get; set; }

        


    }
}
