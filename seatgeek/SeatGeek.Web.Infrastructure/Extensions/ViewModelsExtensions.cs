using SeatGeek.Web.ViewModels.Category.Interfaces;


namespace SeatGeek.Web.Infrastructure.Extensions
{
    public static class ViewModelsExtensions
    {
        public static string GetUrlInformation(this ICategoryDetailsModel model)
        {
            return model.Name.Replace(" ", "-");
        }
    }
}
