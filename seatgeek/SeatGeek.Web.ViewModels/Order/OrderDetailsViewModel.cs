using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeatGeek.Web.ViewModels.Ticket;
using static SeatGeek.Common.EntityValidationConstants.Order;

namespace SeatGeek.Web.ViewModels.Order
{
    public class OrderDetailsViewModel
    {

        public int OrderId { get; set; }

      
        public int EventID { get; set; }


        public DateTime OrderDate { get; set; }


        public string Title { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;


        public int NumberTickets { get; set; }

        public decimal OrderTotal { get; set; }
        public List<TicketFormModel> Tickets { get; set; } = null!;

        
    }
}
