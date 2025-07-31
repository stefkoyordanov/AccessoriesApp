using AccessoriesApp.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessoriesApp.Services.Interfaces
{
    public interface ICategoryService 
    {
        Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync();
        Task<CategoryViewModel?> GetCategoryDetailsByIdAsync(int id);
        Task<bool> AddCategoryAsync(CategoryFormInputModel inputModel);
        Task<CategoryFormInputModel?> GetEditableCategoryByIdAsync(int id);
        Task<bool> EditCategoryAsync(CategoryFormInputModel inputModel);

        Task<CategoryDeleteViewModel?> GetRecipeForDeleteAsync(int id);
        Task<bool> DeleteCategoryAsync(int id);
        Task<bool> EditToggleStatusAsync(int id);
        Task<IEnumerable<CategoryViewModel>> GetMyAllCategoriesAsync();

        Task<bool> CategoryExistsAsync(CategoryFormInputModel inputModel);

    }
}
