namespace SeatGeek.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SeatGeek.Data;
    using SeatGeek.Data.Models;
    using SeatGeek.Services.Data.Interfaces;
    using SeatGeek.Web.ViewModels.Category;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static SeatGeek.Common.EntityValidationConstants;

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


    }
}
