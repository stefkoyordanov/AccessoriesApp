using AccessoriesApp.Data;
using AccessoriesApp.Data.Models;
using AccessoriesApp.GCommon;
using AccessoriesApp.Services.Interfaces;
using AccessoriesApp.Web.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static AccessoriesApp.GCommon.ApplicationConstants;

namespace AccessoriesApp.Services
{
    public class AccessoryService : IAccessoryService
    {
        private readonly ApplicationDbContext _dbContext;

        public AccessoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync()
        {
            return await _dbContext.Categories
                .Where(r => r.IsActive)
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<AccessoriesIndexViewModel>> GetAllAccessoriesAsync(string? userId)
        {
            IEnumerable<AccessoriesIndexViewModel> allAccessories = await this._dbContext
                .Accessories
                .AsNoTracking()
                .Where(r => !r.IsDeleted)
                .Include(r => r.Category)
                .Include(r => r.UserAccessories)
                .Select(m => new AccessoriesIndexViewModel()
                {
                    Id = m.Id.ToString(),
                    Title = m.Title,
                    Description = m.Description,
                    CategoryName = m.Category.Name,
                    ReleaseDate = m.ReleaseDate.ToString(AppDateFormat),
                    PriceBGN = m.PriceBGN.ToString(),
                    PriceEuro = m.PriceEuro.ToString(),
                    Image = m.Image,

                    IsAuthor = userId != null && m.AuthorId == userId,
                    IsSaved = userId != null && m.UserAccessories.Any(ur => ur.UserId == userId),
                    SavedCount = m.UserAccessories.Count

                })
                .ToListAsync();

            /*
            foreach (AccessoriesIndexViewModel movie in allAccessories)
            {
                if (String.IsNullOrEmpty(movie.ImageUrl))
                {
                    movie.ImageUrl = $"/images/{NoImageUrl}";
                }
            }
            */

            return allAccessories;
        }

        public async Task<AccessoriesDetailsViewModel?> GetAccessoryDetailsByIdAsync(int id)
        {
            AccessoriesDetailsViewModel? movieDetails = null;
            
                movieDetails = await this._dbContext
                    .Accessories                    
                    .AsNoTracking()
                    .Where(m => m.Id == id && !m.IsDeleted)
                    .Select(m => new AccessoriesDetailsViewModel()
                    {
                        Id = m.Id.ToString(),
                        Title = m.Title,
                        Description = m.Description,
                        CategoryName = m.Category.Name,                        
                        ReleaseDate = m.ReleaseDate,
                        PriceBGN = m.PriceBGN.ToString(),                        
                        Image = m.Image                        
                    })
                    .SingleOrDefaultAsync();            

            return movieDetails;
        }


        
        public async Task<bool> AddAccessoryAsync(AccessoriesFormInputModel inputModel,string userId)
        {
            bool opResult = false;
            Accessory newAccessory = new Accessory()
            {
                Title = inputModel.Title,
                CategoryId = inputModel.CategoryId,
                ReleaseDate = DateOnly
                    .ParseExact(inputModel.ReleaseDate, AppDateFormat,
                        CultureInfo.InvariantCulture, DateTimeStyles.None),
                PriceBGN = Convert.ToDecimal(inputModel.PriceBGN),
                Description = inputModel.Description,
                ImageFileName = inputModel.ImageFileName,
                TypeImage = inputModel.TypeImage,
                Image = inputModel.Image,
                AuthorId = userId
            };

            await this._dbContext.Accessories.AddAsync(newAccessory);
            await this._dbContext.SaveChangesAsync();
            opResult = true;

            return opResult;
        }


        
        public async Task<AccessoriesFormInputModel?> GetEditableAccessoryByIdAsync(int id)
        {
            AccessoriesFormInputModel? editableMovie = null;
            
                editableMovie = await this._dbContext
                    .Accessories
                    .AsNoTracking()
                    .Where(m => m.Id == id)
                    .Select(m => new AccessoriesFormInputModel()
                    {
                        //Description = m.Description,
                        //TypeAccessory = m.TypeAccessory,
                        //ImageUrl = m.ImageUrl ?? $"/images/{NoImageUrl}",
                       // ReleaseDate = m.ReleaseDate.ToString(AppDateFormat),
                        //PriceEuro = m.PriceEuro.ToString(),
                        //Title = m.Title,
                                                
                        Title = m.Title,
                        Description = m.Description,
                        CategoryId = m.CategoryId,
                        ReleaseDate = m.ReleaseDate.ToString(ApplicationConstants.AppDateFormat),
                        PriceBGN = m.PriceBGN.ToString(),
                        Image = m.Image,
                    })
                    .SingleOrDefaultAsync();
            

            return editableMovie;
        }



        
        public async Task<bool> EditAccessoryAsync(AccessoriesFormInputModel inputModel)
        {
            Accessory? editableMovie = await this._dbContext
                .Accessories
                .SingleOrDefaultAsync(m => m.Id.ToString() == inputModel.Id);
            if (editableMovie == null)
            {
                return false;
            }

            DateOnly movieReleaseDate = DateOnly
                .ParseExact(inputModel.ReleaseDate, AppDateFormat,
                    CultureInfo.InvariantCulture, DateTimeStyles.None);
            editableMovie.Title = inputModel.Title;
            editableMovie.Description = inputModel.Description;            
            editableMovie.TypeAccessory = inputModel.TypeAccessory;
            editableMovie.ImageUrl = inputModel.ImageUrl ?? $"/images/{NoImageUrl}";
            editableMovie.ReleaseDate = movieReleaseDate;

            await this._dbContext.SaveChangesAsync();

            return true;
        }




        
        public async Task<int> DeleteAccessoryAsync(int id)
        {
            Accessory? deletableMovie = await this._dbContext
                .Accessories
                .SingleOrDefaultAsync(m => m.Id == id);
            if (deletableMovie == null)
            {
                return 0;
            }
            _dbContext.Accessories.Remove(deletableMovie);
            int countdeleted = await this._dbContext.SaveChangesAsync();

            return countdeleted;
        }

        public Task<AccessoriesFormInputModel?> GetEditableAccessoryByIdAsync(string? id)
        {
            throw new NotImplementedException();
        }

        
    }
}
