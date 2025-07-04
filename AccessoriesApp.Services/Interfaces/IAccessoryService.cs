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

        Task<IEnumerable<AccessoriesIndexViewModel>> GetAllAccessoriesAsync();

        //Task AddAccessoryAsync(AccessoriesFormInputModel inputModel);

        Task<AccessoriesDetailsViewModel?> GetAccessoryDetailsByIdAsync(int id);

        //Task<AccessoriesFormInputModel?> GetEditableAccessoryByIdAsync(string? id);

        //Task<bool> EditAccessoryAsync(AccessoriesFormInputModel inputModel);
        //Task<int> DeleteAccessoryAsync(string? id);

    }
}
