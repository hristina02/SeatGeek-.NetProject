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

        Task<IEnumerable<TicketFormModel>> GetTicketsAsync(string eventId);
       // Task<bool> UserExistsByIdAsync(string userId);
    }
}
