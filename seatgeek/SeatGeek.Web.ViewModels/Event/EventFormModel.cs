namespace SeatGeek.Web.ViewModels.Event
{
    using Microsoft.Extensions.Logging.Abstractions;
    using SeatGeek.Web.ViewModels.Category;
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Event;
    public class EventFormModel
    {
        public EventFormModel()
        {

            Categories = new List<IndexCategoryFormModel>();
           // Tickets=new List<TicketFormModel>();
        }

        [Required]
        [StringLength(TitleMaxLength,MinimumLength =TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(CityMaxLength, MinimumLength = CityMinLength)]
        public string City { get; set; }=null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(1, MaxCapacityConst, ErrorMessage = "Quantity must be between 1 and {1}")]
        public int MaxCapacity { get; set; } 

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public List<TicketFormModel> Tickets{ get; set; } = null!;

       public IEnumerable<IndexCategoryFormModel> Categories { get; set; } = null!;




    }
}
