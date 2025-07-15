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
    public class CategoryService : ICategoryService
    {

        private readonly ApplicationDbContext _dbContext;

        public CategoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync()
        {
            return await _dbContext.Categories                
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<CategoryViewModel?> GetCategoryFormInputModelDetailsByIdAsync(int id)
        {
            return await _dbContext.Categories
                .AsNoTracking()
                .Where(m => m.Id == id)
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .SingleOrDefaultAsync();
        }

        public async Task<bool> AddCategoryAsync(CategoryFormInputModel inputModel)
        {
            bool opResult = false;
            Category category = new Category()
            {
                Name = inputModel.Name
            };
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            opResult = true;
            return opResult;
        }

        public async Task<CategoryFormInputModel?> GetEditableCategoryByIdAsync(int id)
        {
            return await _dbContext.Categories
                .AsNoTracking()
                .Where(m => m.Id == id)
                .Select(c => new CategoryFormInputModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .SingleOrDefaultAsync();
        }

        public async Task<bool> EditCategoryAsync(CategoryFormInputModel inputModel)
        {
            Category? editableCategory = await this._dbContext
                 .Categories
                 .SingleOrDefaultAsync(m => m.Id == inputModel.Id);
            if (editableCategory == null)
            {
                return false;
            }

            editableCategory.Name = inputModel.Name;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public Task<CategoryViewModel?> GetCategoryDetailsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryDeleteViewModel?> GetRecipeForDeleteAsync(int id)
        {
            var category = await _dbContext.Categories
                .Where(m => m.Id == id)
                .Select(c => new CategoryDeleteViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .SingleOrDefaultAsync();
            return category;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);

            if (category == null)
            {
                return false; // Recipe not in favorites
            }

            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();
            }
            return true;
        }




    }
}
