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

    }
}
