namespace SeatGeek.Web.ViewModels.Home
{
    using SeatGeek.Services.Mapping;
    using Data.Models;
    public class IndexViewModel:IMapFrom<Event>
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;
    }
}
