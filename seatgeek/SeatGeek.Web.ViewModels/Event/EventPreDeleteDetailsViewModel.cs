namespace SeatGeek.Web.ViewModels.Event
{
    using System.ComponentModel.DataAnnotations;
    public class EventPreDeleteDetailsViewModel
    {

        public string Title { get; set; } = null!;

        public string Address { get; set; } = null!;

        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;
    }
}
