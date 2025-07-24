using AccessoriesApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccessoriesApp.Web.ViewComponents
{
    public class CategoryDropdownViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoryDropdownViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var categories = new List<string> { "Phones", "Laptops", "Cameras" }; // Or fetch from DB
            //return View(categories);
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }

    }
}
