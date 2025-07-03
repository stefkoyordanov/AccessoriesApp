using AccessoriesApp.Data;
using AccessoriesApp.Data.Models;
using AccessoriesApp.Services.Interfaces;
using AccessoriesApp.Web.ViewModels.Accessory;
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

        public async Task<IEnumerable<AccessoriesIndexViewModel>> GetAllAccessoriesAsync()
        {
            IEnumerable<AccessoriesIndexViewModel> allAccessories = await this._dbContext
                .Accessories
                .AsNoTracking()
                .Select(m => new AccessoriesIndexViewModel()
                {
                    Id = m.Id.ToString(),
                    Title = m.Title,
                    TypeAccessory = m.TypeAccessory,
                    ReleaseDate = m.ReleaseDate.ToString(AppDateFormat),
                    PriceEuro = m.PriceEuro.ToString(),
                    ImageUrl = m.ImageUrl,
                })
                .ToListAsync();
            foreach (AccessoriesIndexViewModel movie in allAccessories)
            {
                if (String.IsNullOrEmpty(movie.ImageUrl))
                {
                    movie.ImageUrl = $"/images/{NoImageUrl}";
                }
            }

            return allAccessories;
        }

        public async Task AddAccessoryAsync(AccessoriesFormInputModel inputModel)
        {
            Accessory newMovie = new Accessory()
            {
                Title = inputModel.Title,
                TypeAccessory = inputModel.TypeAccessory,
                PriceEuro = Convert.ToDecimal(inputModel.PriceEuro),
                Description = inputModel.Description,
                ImageUrl = inputModel.ImageUrl,
                ReleaseDate = DateOnly
                    .ParseExact(inputModel.ReleaseDate, AppDateFormat,
                        CultureInfo.InvariantCulture, DateTimeStyles.None),
            };

            await this._dbContext.Accessories.AddAsync(newMovie);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task<AccessoriesDetailsViewModel?> GetAccessoryDetailsByIdAsync(string? id)
        {
            AccessoriesDetailsViewModel? movieDetails = null;

            bool isIdValidGuid = Guid.TryParse(id, out Guid movieId);
            if (isIdValidGuid)
            {
                movieDetails = await this._dbContext
                    .Accessories
                    .AsNoTracking()
                    .Where(m => m.Id == movieId)
                    .Select(m => new AccessoriesDetailsViewModel()
                    {
                        Id = m.Id.ToString(),
                        Description = m.Description,
                        TypeAccessory = m.TypeAccessory,
                        ImageUrl = m.ImageUrl ?? $"/images/{NoImageUrl}",
                        ReleaseDate = m.ReleaseDate.ToString(AppDateFormat),
                        PriceEuro = m.PriceEuro.ToString(),
                        Title = m.Title
                    })
                    .SingleOrDefaultAsync();
            }

            return movieDetails;
        }

                

        public async Task<AccessoriesFormInputModel?> GetEditableAccessoryByIdAsync(string? id)
        {
            AccessoriesFormInputModel? editableMovie = null;

            bool isIdValidGuid = Guid.TryParse(id, out Guid movieId);
            if (isIdValidGuid)
            {
                editableMovie = await this._dbContext
                    .Accessories
                    .AsNoTracking()
                    .Where(m => m.Id == movieId)
                    .Select(m => new AccessoriesFormInputModel()
                    {
                        Description = m.Description,
                        TypeAccessory = m.TypeAccessory,
                        ImageUrl = m.ImageUrl ?? $"/images/{NoImageUrl}",
                        ReleaseDate = m.ReleaseDate.ToString(AppDateFormat),
                        PriceEuro = m.PriceEuro.ToString(),
                        Title = m.Title
                    })
                    .SingleOrDefaultAsync();
            }

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


        public async Task<int> DeleteAccessoryAsync(string? id)
        {
            Accessory? deletableMovie = await this._dbContext
                .Accessories
                .SingleOrDefaultAsync(m => m.Id.ToString() == id);
            if (deletableMovie == null)
            {
                return 0;
            }
            _dbContext.Accessories.Remove(deletableMovie);
            int countdeleted = await this._dbContext.SaveChangesAsync();

            return countdeleted;
        }




    }
}
