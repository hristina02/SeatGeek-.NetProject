namespace SeatGeek.Services.Data.Interfaces
{
    using SeatGeek.Web.ViewModels.Event;
    using SeatGeek.Web.ViewModels.Home;
    public interface IEventService
    {
        Task<IEnumerable<IndexViewModel>> LastFiveEventsAsync();
        Task<string> CreateAndReturnIdAsync(EventFormModel formModel, string agentId);
    }
}
