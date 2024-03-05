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

        Task<EventDetailsViewModel> GetDetailsByIdAsync(string eventId);

        Task<bool> ExistsByIdAsync(string eventId);

        Task<bool> IsAgentWithIdOwnerOfEventWithIdAsync(string eventId, string agentId);

        Task<EventFormModel> GetEventForEditByIdAsync(string eventId);

        Task EditEventByIdAndFormModelAsync(string eventId, EventFormModel formModel);

        Task<EventPreDeleteDetailsViewModel> GetEventForDeleteByIdAsync(string eventId);

        Task DeleteEventByIdAsync(string eventId);
    }
}
