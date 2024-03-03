namespace SeatGeek.Services.Data.Interfaces
{
    using SeatGeek.Services.Data.Model.Event;
    using SeatGeek.Web.ViewModels.Event;
    using SeatGeek.Web.ViewModels.Home;
    public interface IEventService
    {
        Task<IEnumerable<IndexViewModel>> LastFiveEventsAsync();
        Task<string> CreateAndReturnIdAsync(EventFormModel formModel, string agentId);

        Task<AllEventsFilteredAndPagedServiceModel> AllAsync(AllEventsQueryModel queryModel);

        Task<IEnumerable<EventAllViewModel>> AllByAgentIdAsync(string agentId);

        Task<IEnumerable<EventAllViewModel>> AllByUserIdAsync(string userId);
    }
}
