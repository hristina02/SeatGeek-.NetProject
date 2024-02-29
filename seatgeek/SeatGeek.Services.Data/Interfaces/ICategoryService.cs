namespace SeatGeek.Services.Data.Interfaces
{
    using SeatGeek.Data.Models;
    using SeatGeek.Web.ViewModels.Category;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static SeatGeek.Common.EntityValidationConstants;

    public interface ICategoryService
    {
        Task<IEnumerable<IndexCategoryFormModel>> AllCategoriesAsync();

        Task<bool> ExistsByIdAsync(int id);

    }
}
