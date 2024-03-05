
using SeatGeek.Data;
using SeatGeek.Services.Data.Interfaces;
namespace SeatGeek.Services.Data
{
    public class TicketService:ITicketService
    {

        private readonly SeatGeekDbContext dbContext;

        public TicketService(SeatGeekDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //public async Task<bool> UserExistsByIdAsync(string userId)
        //{
        //    bool result = await this.dbContext
        //                      .
                
        //    return result;
        //}
    }
}
