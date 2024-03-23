using SeatGeek.Web.ViewModels.User;

namespace SeatGeek.Services.Data.Interfaces
{
    public interface IUserService
    {

        Task<string> GetFullNameByEmailAsync(string email);

        Task<string> GetFullNameByIdAsync(string userId);
        Task<IEnumerable<UserViewModel>> AllAsync();
    }
}
