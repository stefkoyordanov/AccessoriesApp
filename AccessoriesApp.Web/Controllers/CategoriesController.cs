using AccessoriesApp.Data;
using AccessoriesApp.Data.Models;
using AccessoriesApp.Services.Interfaces;
using AccessoriesApp.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccessoriesApp.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int id)
        {            
            var category = await _categoryService.GetCategoryDetailsByIdAsync(id);
                
            if (category == null) return NotFound();

            return View(category);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            var model = new CategoryFormInputModel();            
            return View(model);            
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CategoryFormInputModel category)
        {
            if (await _categoryService.CategoryExistsAsync(category))
            {
                ModelState.AddModelError("Name", "A category with this name already exists.");
            }

            if (ModelState.IsValid)
            {
                await _categoryService.AddCategoryAsync(category);
                return RedirectToAction(nameof(MyAllCategories));
            }
            return View(category);
        }

        // GET: Category/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        { 
            CategoryFormInputModel? editableCategory = await this._categoryService
                    .GetEditableCategoryByIdAsync(id);
            if (editableCategory == null)
            {
                // TODO: Custom 404 page
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(editableCategory);
            
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(CategoryFormInputModel inputModel)
        {

            if (!ModelState.IsValid)
            {                
                return View(inputModel);
            }
            
                try
                {
                    bool editSuccess = await this._categoryService.EditCategoryAsync(inputModel);
                    if (!editSuccess)
                    {
                        // TODO: Custom 404 page
                        return this.RedirectToAction(nameof(Index));
                    }

                    return this.RedirectToAction(nameof(Details), new { id = inputModel.Id });
                }
                catch (Exception e)
                {
                    // TODO: Implement it with the ILogger
                    // TODO: Add JS bars to indicate such errors
                    Console.WriteLine(e.Message);

                    return View(inputModel);
                }     
        }



        // GET: Category/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            CategoryDeleteViewModel? deleteCategory = await this._categoryService
                    .GetRecipeForDeleteAsync(id);
            if (deleteCategory == null)
            {
                // TODO: Custom 404 page
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(deleteCategory);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {            
            try
            {
                bool deleteSuccess = await this._categoryService.DeleteCategoryAsync(id);
                if (!deleteSuccess)
                {
                    // TODO: Custom 404 page
                    return this.RedirectToAction(nameof(Index));
                }

                return this.RedirectToAction(nameof(Details), new { id = id });
            }
            catch (Exception e)
            {
                // TODO: Implement it with the ILogger
                // TODO: Add JS bars to indicate such errors
                Console.WriteLine(e.Message);

                return RedirectToAction(nameof(Index));
            }
                        
        }

        // GET: Category
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> MyAllCategories()
        {
            var categories = await _categoryService.GetMyAllCategoriesAsync();
            return View(categories);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ToggleStatus(int id)
        { 
            bool toggleSuccess = await this._categoryService.EditToggleStatusAsync(id);
            if (!toggleSuccess)
            {
                // TODO: Custom 404 page
                return this.RedirectToAction(nameof(MyAllCategories));
            }

            return RedirectToAction(nameof(MyAllCategories));
        }




        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }



    }
}
