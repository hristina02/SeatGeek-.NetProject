namespace SeatGeek.Services.Data.Interfaces
{
    using SeatGeek.Web.ViewModels.Agent;
    public interface IAgentService
    {
        Task<bool> AgentExistsByUserIdAsync(string userId);

        Task<bool> AgentExistsByPhoneNumberAsync(string phoneNumber);

        Task Create(string userId, BecomeAgentFormModel model);
    }
}
