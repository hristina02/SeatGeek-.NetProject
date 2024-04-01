using SeatGeek.Web.ViewModels.User;

namespace SeatGeek.Services.Data.Interfaces
{
    public interface IUserService
    {

        Task<string> GetFullNameByEmailAsync(string email);

        Task<string> GetFullNameByIdAsync(string userId);
        Task<string> GetNameByEmailAsync(string email);

        Task<IEnumerable<UserViewModel>> AllAsync();
    }
}
