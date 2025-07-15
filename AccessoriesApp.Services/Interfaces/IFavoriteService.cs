using AccessoriesApp.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessoriesApp.Services.Interfaces
{
    public interface IFavoriteService
    {
        Task<bool> AddToFavoritesAsync(int id, string userId);
        Task<IEnumerable<AccessoriesIndexViewModel>> GetFavoriteAsync(string userId);
        

        Task<bool> RemoveFromFavoritesAsync(int id, string userId);
    }
}
