using Microsoft.AspNetCore.Mvc;
using SeatGeek.Data.Models.Enums;
using SeatGeek.Services.Data.Interfaces;
using SeatGeek.Web.Infrastructure.Extensions;
using static SeatGeek.Common.NotificationMessagesConstants;
using static SeatGeek.Common.EntityValidationConstants.Event;
using static SeatGeek.Common.GeneralApplicationConstants;
using SeatGeek.Web.ViewModels.Category;

using Microsoft.Extensions.Caching.Memory;
namespace SeatGeek.Web.Areas.Admin.Controllers
{
    public class CategoryController : BaseAdminController
    {

        private readonly IAgentService agentService;
        private readonly IEventService eventService;
        private readonly ICategoryService categoryService;
        private readonly IMemoryCache memoryCache;
        public CategoryController(IAgentService agentService, IEventService eventService, ICategoryService categoryService, IMemoryCache memoryCache)
        {
            this.agentService = agentService;
            this.eventService = eventService;
            this.categoryService = categoryService;
            this.memoryCache = memoryCache;
        }

        [HttpGet]
        [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Client, NoStore = false)]
        public async Task<IActionResult> All()
        {
            IEnumerable<AllCategoriesViewModel> allCategories =
               this.memoryCache.Get<IEnumerable<AllCategoriesViewModel>>(CategoriesCacheKey);

            if (allCategories == null)
            {
                allCategories = await this.categoryService.AllCategoriesForListAsync();

                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(CategoriesCacheDurationMinutes));

                this.memoryCache.Set(UsersCacheKey, allCategories, cacheOptions);
            }

            return this.View(allCategories);
           
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            bool isAgent =
                await this.agentService.AgentExistsByUserIdAsync(this.User.GetId()!);
            if (!isAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to add new category!";

                return this.RedirectToAction("Become", "Agent");
            }

           
              var formModel = new IndexCategoryFormModel();
              return (View(formModel));
            
            
        }

        [HttpPost]
        public async Task<IActionResult> Add(IndexCategoryFormModel model)
        {
            bool isAgent =
                await this.agentService.AgentExistsByUserIdAsync(this.User.GetId()!);
            if (!isAgent && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to add new event!";

                return this.RedirectToAction("Become", "Agent");
            }
           

            bool categoryExists =
                await this.categoryService.ExistsByIdAsync(model.Id);
            if (!categoryExists)
            {
                // Adding model error to ModelState automatically makes ModelState Invalid
                this.ModelState.AddModelError(nameof(model.Id), "This category already exist!");
            }

           

            try
            {
                string? agentId =
                    await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);

                string categoryId =
                    await this.categoryService.CreateAndReturnIdAsync(model,agentId);

                this.TempData[SuccessMessage] = "Category was added successfully!";
                return this.RedirectToAction("All", "Category", new { id = categoryId });
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new category!");
               

                return this.View(model);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Category ID is required.");
            }

            try
            {

                await categoryService.DeleteCategoryByIdAsync(id);
                
                return RedirectToAction("All","Category"); 
            }
            catch (Exception)
            {
               this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new category!");
                return RedirectToAction("All", "Category");
            }
        }
        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] =
                "Unexpected error occurred!";

            return this.RedirectToAction("Index", "Home");
        }
    }
}
