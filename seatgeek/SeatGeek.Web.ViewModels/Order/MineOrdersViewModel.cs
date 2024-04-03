
namespace SeatGeek.Web.ViewModels.Order
{
    public class MineOrdersViewModel
    {
        public int Id { get; set; }


        public int EventId { get; set; }


        public DateTime OrderDate { get; set; }


        public string Title { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string Start { get; set; } = null!;

        public string End { get; set; } = null!;
        public int NumberTickets { get; set; }

        public decimal OrderTotal { get; set; }
    }
}
