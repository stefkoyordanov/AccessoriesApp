﻿using AccessoriesApp.Data;
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

        public async Task<IEnumerable<OrderItemDetailsModel>> GetAllOrderItemAsync(string userId)
        {

            var orderItems = await this._dbContext
                .OrderItems
                .Include(oi => oi.OrderItemAccessory)
                .ThenInclude(r => r.Category)                
                .AsNoTracking()
                .Where(m => m.OrderItemUserId == userId && !m.IsOrderItemConfirmed)
                .Select(m => new OrderItemDetailsModel()
                {
                    Id = m.Id,
                    OrderId = m.OrderId,
                    OrderItemUserId = m.OrderItemUserId,
                    OrderItemAccessoryId = m.OrderItemAccessoryId,
                    Title = m.OrderItemAccessory.Title,
                    CategoryName = m.OrderItemAccessory.Category.Name,
                    ReleaseDate = m.OrderItemAccessory.ReleaseDate,
                    Quantity = m.Quantity,
                    PriceBGN = m.PriceBGN,
                    Description = m.OrderItemAccessory.Description,
                    Image = m.OrderItemAccessory.Image
                })
                .ToListAsync();

            return orderItems;

        }



        public async Task<OrderItemFormInputModel> GetOrderItemFormInputByIdAsync(int id, string userId)
        {            

            var accessory = await this._dbContext
                .Accessories
                .AsNoTracking()
                .Where(m => m.Id == id && !m.IsDeleted)
                .Select(m => new OrderItemFormInputModel()
                {
                    OrderItemAccessoryId = m.Id,
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
                OrderItemAccessoryId = accessory.OrderItemAccessoryId,
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


            return orderItem;

        }

        public async Task<bool> AddToOrderItem_OrderAsync(OrderItemFormInputModel inputModel, string userId)
        {
            bool opResult = false;

            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            //var order = new Order();
            try
            {
                bool orderexists = false;

                if ( !_dbContext.Orders.Any(u => u.OrderUserId == userId && u.IsOrderConfirmed == false) )
                {
                    // 1. Create Order
                    var order = new Order
                    {
                        OrderUserId = userId,
                        ConfirmedOn = DateOnly.FromDateTime(DateTime.UtcNow),
                        TotalPriceBGN = inputModel.PriceBGN * inputModel.Quantity,
                        IsOrderConfirmed = false
                    };

                    _dbContext.Orders.Add(order);
                    await _dbContext.SaveChangesAsync(); // Save to get Order.Id

                    // 2. Create OrderItem with the generated Order.Id
                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        OrderItemUserId = userId,
                        OrderItemAccessoryId = inputModel.OrderItemAccessoryId,
                        Quantity = inputModel.Quantity,
                        PriceBGN = inputModel.PriceBGN,
                        CreatedOnOrderItem = DateOnly.FromDateTime(DateTime.UtcNow),
                        IsOrderItemIsActive = true,
                        IsOrderItemConfirmed = false
                    };

                    _dbContext.OrderItems.Add(orderItem);
                    await _dbContext.SaveChangesAsync();

                }
                else
                {                    
                        var order = await _dbContext.Orders
                        .Where(u => u.OrderUserId == userId && u.IsOrderConfirmed == false)
                        .OrderByDescending(u => u.ConfirmedOn) // Optional: get latest if multiple exist
                        .FirstOrDefaultAsync();

                        // Create OrderItem with the generated Order.Id
                        var orderItem = new OrderItem
                        {
                            OrderId = order.Id,
                            OrderItemUserId = userId,
                            OrderItemAccessoryId = inputModel.OrderItemAccessoryId,
                            Quantity = inputModel.Quantity,
                            PriceBGN = inputModel.PriceBGN,
                            IsOrderItemIsActive = true,
                            IsOrderItemConfirmed = false
                        };

                        _dbContext.OrderItems.Add(orderItem);
                        await _dbContext.SaveChangesAsync();


                        var orderItemsData = await this._dbContext
                        .OrderItems
                        .AsNoTracking()
                        .Where(m => m.OrderId == order.Id)
                        .ToListAsync();

                        // Calculate total price
                        decimal totalPriceBGN = orderItemsData.Sum(item => item.Quantity * item.PriceBGN);

                        order.TotalPriceBGN = totalPriceBGN;
                        await _dbContext.SaveChangesAsync();                    
                }

            await transaction.CommitAsync();
            opResult = true;

            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

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

        public async Task<OrderItemResultModel> EditOrderItemsAsync(OrderItemFormInputModel inputModel)
        {
            
            OrderItem ? editableOrderItem = await this._dbContext
                .OrderItems                
                .SingleOrDefaultAsync(m => m.Id == inputModel.Id);                
            if (editableOrderItem == null)
            {
                return new OrderItemResultModel() { rownumberorderitem = 0, rownumberorder = 0, totalpricebgn = 0m };
            }

            
            editableOrderItem.Quantity = inputModel.Quantity;
            int numberorderitem = await _dbContext.SaveChangesAsync();            


            var orderItemsData = await this._dbContext
                        .OrderItems
                        .AsNoTracking()
                        .Where(m => m.OrderId == inputModel.OrderId)
                        .ToListAsync();

            // Calculate total price
            decimal totalPriceBGN = orderItemsData.Sum(item => item.Quantity * item.PriceBGN);

            var order = await _dbContext.Orders                        
                        .Where(u => u.Id == inputModel.OrderId && u.IsOrderConfirmed == false)
                        .OrderByDescending(u => u.ConfirmedOn) // Optional: get latest if multiple exist
                        .FirstOrDefaultAsync();

            order.TotalPriceBGN = totalPriceBGN;
            int numberorder = await _dbContext.SaveChangesAsync();

            var ordertotal = await _dbContext.Orders
                        .AsNoTracking()
                        .Where(u => u.Id == inputModel.OrderId && u.IsOrderConfirmed == false)                        
                        .FirstOrDefaultAsync();

            return new OrderItemResultModel() { rownumberorderitem = numberorderitem, rownumberorder = numberorder, totalpricebgn = ordertotal.TotalPriceBGN };
                        
        }

        public async Task<decimal> TotalSumOrder(int? orderid)
        {
            var ordertotal = await _dbContext.Orders
                        .AsNoTracking()
                        //.Where(u => u.Id == orderid && u.IsOrderConfirmed == false)
                        .Where(u => u.Id == orderid)
                        .FirstOrDefaultAsync();

            return ordertotal.TotalPriceBGN;
        }


        public async Task<OrderItemDetailsModel?> GetOrderItemForDeleteAsync(int id, string userId)
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

            var orderItem = await _dbContext.OrderItems                
                .AsNoTracking()
                .Where(r => r.Id == id && r.OrderItemUserId == userId)                
                .FirstOrDefaultAsync();


            OrderItem? deletableOrderItem = await this._dbContext
                .OrderItems
                .SingleOrDefaultAsync(m => m.Id == id);
            if (deletableOrderItem == null)
            {
                return 0;
            }
            _dbContext.OrderItems.Remove(deletableOrderItem);
            int countdeleted = await this._dbContext.SaveChangesAsync();


            var orderItemsData = await this._dbContext
                        .OrderItems
                        .AsNoTracking()
                        .Where(m => m.OrderId == orderItem.OrderId)
                        .ToListAsync();

            if (orderItemsData.Count > 0)
            {
                // Calculate total price
                decimal totalPriceBGN = orderItemsData.Sum(item => item.Quantity * item.PriceBGN);

                var order = await _dbContext.Orders
                            .Where(u => u.Id == orderItem.OrderId && u.IsOrderConfirmed == false)
                            .OrderByDescending(u => u.ConfirmedOn) // Optional: get latest if multiple exist
                            .FirstOrDefaultAsync();

                order.TotalPriceBGN = totalPriceBGN;
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                // Calculate total price
                decimal totalPriceBGN = orderItemsData.Sum(item => item.Quantity * item.PriceBGN);

                var order = await _dbContext.Orders
                            .Where(u => u.Id == orderItem.OrderId && u.IsOrderConfirmed == false)
                            .OrderByDescending(u => u.ConfirmedOn) // Optional: get latest if multiple exist
                            .FirstOrDefaultAsync();

                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync();

            }

            return countdeleted;
        }

        
    }
}
