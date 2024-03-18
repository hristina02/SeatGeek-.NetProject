namespace SeatGeek.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SeatGeek.Data;
    using SeatGeek.Services.Data.Interfaces;
    using SeatGeek.Web.ViewModels.Category;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Category = SeatGeek.Data.Models.Category;

    public class CategoryService : ICategoryService
    {
        private readonly SeatGeekDbContext dbContext;

        public CategoryService(SeatGeekDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<IndexCategoryFormModel>> AllCategoriesAsync()
        {
            IEnumerable<IndexCategoryFormModel> allCategories = await this.dbContext
                .Categories
                .AsNoTracking()
                .Select(c => new IndexCategoryFormModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToArrayAsync();

            return allCategories;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            bool result = await this.dbContext
                .Categories
                .AnyAsync(c => c.Id == id);

            return result;
        }

        public async Task<IEnumerable<string>> AllCategoryNamesAsync()
        {
            IEnumerable<string> allNames = await this.dbContext
                .Categories
                .Select(c => c.Name)
                .ToArrayAsync();

            return allNames;
        }

        public async Task<CategoryDetailsViewModel> GetDetailsByIdAsync(int id)
        {
            Category category = await this.dbContext
                .Categories
                .FirstAsync(c => c.Id == id);

            CategoryDetailsViewModel viewModel = new CategoryDetailsViewModel()
            {
                Id = category.Id,
                Name = category.Name
            };
            return viewModel;
        }

        public async Task<IEnumerable<AllCategoriesViewModel>> AllCategoriesForListAsync()
        {
            IEnumerable<AllCategoriesViewModel> allCategories = await this.dbContext
              .Categories
              .AsNoTracking()
              .Select(c => new AllCategoriesViewModel()
              {
                  Id = c.Id,
                  Name = c.Name,
              })
              .ToArrayAsync();

            return allCategories;

        }
    }
}
