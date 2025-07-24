using AccessoriesApp.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessoriesApp.Services.Interfaces
{
    public interface IAccessoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync();
        Task<IEnumerable<AccessoriesIndexViewModel>> GetAllAccessoriesAsync(string? userId, int categoryid);

        Task<bool> AddAccessoryAsync(AccessoriesFormInputModel inputModel, string userId);

        Task<AccessoriesDetailsViewModel?> GetAccessoryDetailsByIdAsync(int id, string? userId);

        Task<AccessoriesFormInputModel?> GetEditableAccessoryByIdAsync(string? id);

        Task<bool> EditAccessoryAsync(AccessoriesFormInputModel inputModel);
        Task<int> DeleteAccessoryAsync(int id);

    }
}
