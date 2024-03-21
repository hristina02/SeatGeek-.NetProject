namespace SeatGeek.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SeatGeek.Data;
    using SeatGeek.Data.Models;
    using SeatGeek.Services.Data.Interfaces;
    public class UserService:IUserService
    {
        private readonly SeatGeekDbContext dbContext;

        public UserService(SeatGeekDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> GetFullNameByEmailAsync(string email)
        {
            ApplicationUser? user = await dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return string.Empty;
            }

            return $"{user.FirstName} {user.LastName}";
        }
    }
}
