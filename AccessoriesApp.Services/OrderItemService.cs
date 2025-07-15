using AccessoriesApp.Data;
using AccessoriesApp.Data.Models;
using AccessoriesApp.Services.Interfaces;
using AccessoriesApp.Web.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessoriesApp.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderItemService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderItemFormInputModel> GetOrderItemFormInputByIdAsync(int id, string userId)
        {
            OrderItemFormInputModel? orderItemDetails = null;

            var accessory = await this._dbContext
                .Accessories
                .AsNoTracking()
                .Where(m => m.Id == id && !m.IsDeleted)
                .Select(m => new OrderItemFormInputModel()
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    CategoryName = m.Category.Name,
                    ReleaseDate = m.ReleaseDate,
                    PriceBGN = m.PriceBGN,
                    Image = m.Image
                })
                .SingleOrDefaultAsync();

            var orderItem = new OrderItemFormInputModel
            {
                OrderItemAccessoryId = accessory.Id,
                OrderItemUserId = userId,
                Title = accessory.Title,
                Description = accessory.Description,
                CategoryName = accessory.CategoryName,
                ReleaseDate = accessory.ReleaseDate,
                PriceBGN = accessory.PriceBGN,
                Image = accessory.Image,

                OrderId = null // Optional: associate with an existing Order later
            };


            /*
            orderItemDetails = await _dbContext
            .OrderItems
            .Include(oi => oi.Order)
            .Include(oi => oi.OrderItemAccessory)
            .Include(oi => oi.OrderItemUser)
            .AsNoTracking()
            .Where(m => m.Id == id)
            .Select(m => new OrderItemFormInputModel()
            {
                Id = m.Id,
                Title = m.OrderItemAccessory.Title,
                Description = m.OrderItemAccessory.Description,
                CategoryName = m.OrderItemAccessory.Category.Name,
                ReleaseDate = m.OrderItemAccessory.ReleaseDate,
                Quantity = m.Quantity.ToString(),
                PriceBGN = m.OrderItemAccessory.PriceBGN.ToString(),
                Image = m.OrderItemAccessory.Image
            })
            .SingleOrDefaultAsync();
            */


            return orderItemDetails;

        }

        public async Task<bool> AddToOrderItemAsync(OrderItemFormInputModel inputModel, string userId)
        {
            bool opResult = false;
            OrderItem newAccessory = new OrderItem()
            {
                OrderItemUserId = userId,
                OrderItemAccessoryId = inputModel.OrderItemAccessoryId,
                Quantity = inputModel.Quantity,
                //PriceBGN = Convert.ToDecimal(inputModel.PriceBGN)
            };

            await this._dbContext.OrderItems.AddAsync(newAccessory);
            await this._dbContext.SaveChangesAsync();
            opResult = true;

            return opResult;
        }



        public async Task<OrderItemFormInputModel?> GetEditableOrderItemByIdAsync(int id)
        {
            OrderItemFormInputModel? movieDetails = null;

            movieDetails = await this._dbContext
                .OrderItems
                .Include(oi => oi.OrderItemAccessory)
                .AsNoTracking()
                .Where(m => m.Id == id)
                .Select(m => new OrderItemFormInputModel()
                {
                    Id = m.Id,
                    Title = m.OrderItemAccessory.Title,
                    Description = m.OrderItemAccessory.Description,
                    CategoryName = m.OrderItemAccessory.Category.Name,
                    ReleaseDate = m.OrderItemAccessory.ReleaseDate,
                    Quantity = m.Quantity,
                    PriceBGN = m.PriceBGN,
                    Image = m.OrderItemAccessory.Image
                })
                .SingleOrDefaultAsync();

            return movieDetails;
        }

        public async Task<bool> EditAccessoryAsync(OrderItemFormInputModel inputModel)
        {
            OrderItem? editableOrderItem = await this._dbContext
                .OrderItems
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == inputModel.Id);
            if (editableOrderItem == null)
            {
                return false;
            }

            editableOrderItem.Quantity = inputModel.Quantity;

            await this._dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<OrderItemDetailsModel?> GetRecipeForDeleteAsync(int id, string userId)
        {

            return await _dbContext.OrderItems
                .Include(oi => oi.OrderItemAccessory)
                .AsNoTracking()
                .Where(r => r.Id == id && r.OrderItemUserId == userId)
                .Select(m => new OrderItemDetailsModel
                {
                    Id = m.Id,
                    Title = m.OrderItemAccessory.Title,
                    Description = m.OrderItemAccessory.Description,
                    CategoryName = m.OrderItemAccessory.Category.Name,
                    ReleaseDate = m.OrderItemAccessory.ReleaseDate,
                    Quantity = m.Quantity,
                    PriceBGN = m.PriceBGN,
                    Image = m.OrderItemAccessory.Image
                })
                .FirstOrDefaultAsync();
        }



        public async Task<int> RemoveFromOrderItemAsync(int id, string userId)
        {
            OrderItem? deletableOrderItem = await this._dbContext
                .OrderItems
                .SingleOrDefaultAsync(m => m.Id == id);
            if (deletableOrderItem == null)
            {
                return 0;
            }
            _dbContext.OrderItems.Remove(deletableOrderItem);
            int countdeleted = await this._dbContext.SaveChangesAsync();

            return countdeleted;
        }
    }
}
