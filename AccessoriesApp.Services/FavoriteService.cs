using AccessoriesApp.Data;
using AccessoriesApp.Data.Models;
using AccessoriesApp.Services.Interfaces;
using AccessoriesApp.Web.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessoriesApp.Services
{
    public class FavoriteService : IFavoriteService
    {

        private readonly ApplicationDbContext _dbContext;

        public FavoriteService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddToFavoritesAsync(int id, string userId)
        {
            var favorite = await _dbContext.Accessories
                .Where(r => r.Id == id && !r.IsDeleted && r.AuthorId != userId)
                .FirstOrDefaultAsync();

            if (favorite == null)
            {
                return false; // Accessory not found, deleted, or user is the author
            }

            var existingFavorite = await _dbContext.UserAccessories
                .AnyAsync(ur => ur.AccessoryId == id && ur.UserId == userId);

            if (existingFavorite)
            {
                return false; // Accessory already in favorites
            }

            var userFavorite = new UserAccessory
            {
                UserId = userId,
                AccessoryId = id                
            };

            await _dbContext.UserAccessories.AddAsync(userFavorite);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<AccessoriesIndexViewModel>> GetFavoriteAsync(string userId)
        {
            return await _dbContext.UserAccessories
                .Where(ur => ur.UserId == userId)
                .Include(ur => ur.Accessory)
                .ThenInclude(r => r.Category)
                .Include(ur => ur.Accessory.UserAccessories)
                .Where(ur => !ur.Accessory.IsDeleted)
                .Select(ur => new AccessoriesIndexViewModel
                {
                    Id = ur.Accessory.Id.ToString(),
                    Title = ur.Accessory.Title,
                    Image = ur.Accessory.Image,
                    CategoryName = ur.Accessory.Category.Name,
                    IsAuthor = ur.Accessory.AuthorId == userId,
                    IsSaved = true // All accessories here are favorites
                })
                .ToListAsync();
        }

        public async Task<bool> RemoveFromFavoritesAsync(int id, string userId)
        {
            var userRecipe = await _dbContext.UserAccessories
                .Where(ur => ur.AccessoryId == id && ur.UserId == userId)
                .FirstOrDefaultAsync();

            if (userRecipe == null)
            {
                return false; // Recipe not in favorites
            }

            _dbContext.UserAccessories.Remove(userRecipe);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
