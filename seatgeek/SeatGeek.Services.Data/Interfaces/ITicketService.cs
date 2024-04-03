using SeatGeek.Web.ViewModels.Event;
using SeatGeek.Web.ViewModels.Order;
using SeatGeek.Web.ViewModels.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatGeek.Services.Data.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<MineOrdersViewModel> >AllOrderedTicketsByUserIdAsync(string userId);
        Task<IEnumerable<TicketFormModel>> GetTicketsAsync(string eventId);
        Task<string>CreateOrderIdAsync(OrderFormModel orderModel,string userId);

        Task<OrderDetailsViewModel> GetDetailsByIdAsync(string orderId);
    }
}
