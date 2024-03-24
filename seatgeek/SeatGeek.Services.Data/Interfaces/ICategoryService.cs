namespace SeatGeek.Services.Data.Interfaces
{
    using SeatGeek.Data.Models;
    using SeatGeek.Web.ViewModels.Category;
    using SeatGeek.Web.ViewModels.Event;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static SeatGeek.Common.EntityValidationConstants;

    public interface ICategoryService
    {
        Task<IEnumerable<IndexCategoryFormModel>> AllCategoriesAsync();

        Task<IEnumerable<AllCategoriesViewModel>> AllCategoriesForListAsync();

        Task<string> CreateAndReturnIdAsync(IndexCategoryFormModel formModel,string agentId);

        Task<bool> ExistsByIdAsync(int id);

        Task<IEnumerable<string>> AllCategoryNamesAsync();

        Task<CategoryDetailsViewModel> GetDetailsByIdAsync(int id);

        Task DeleteCategoryByIdAsync(string categoryId);

    }
}
