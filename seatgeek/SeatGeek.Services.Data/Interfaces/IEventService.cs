namespace SeatGeek.Services.Data.Interfaces
{
    using SeatGeek.Web.ViewModels.Home;
    public interface IEventService
    {
        Task<IEnumerable<IndexViewModel>> LastFiveEventsAsync();
    }
}
