﻿using AccessoriesApp.Data;
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
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<OrderItemDetailsModel>> GetOrderItemsInOrderAsync(string userId, int? orderid)
        {
            var orderItems = await this._dbContext
                 .OrderItems
                 .Include(oi => oi.OrderItemAccessory)
                 .ThenInclude(r => r.Category)
                 .AsNoTracking()
                 .Where(m => (m.OrderItemUserId == userId || userId == "0") && m.OrderId == orderid)
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
        public async Task<IEnumerable<CourierViewModel>> GetAllCouriersAsync()
        {
            return await _dbContext.Couriers
                .Where(r => r.IsActive)
                .Select(c => new CourierViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
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



        


        public async Task<OrderDetailsModel> ConfirmOrderAsync(OrderFormInputModel orderFormInputModel, string userId)
        {
                var order = await _dbContext.Orders
                            //.Where(u => u.Id == orderFormInputModel.Id && u.IsOrderConfirmed == false)
                            .Where(u => u.Id == orderFormInputModel.Id )
                            .OrderByDescending(u => u.ConfirmedOn) // Optional: get latest if multiple exist
                            .FirstOrDefaultAsync();

                order.CourierId = orderFormInputModel.CourierId;
                order.ConfirmedOn = DateOnly.FromDateTime(DateTime.UtcNow);
                order.IsOrderConfirmed = true;
                await _dbContext.SaveChangesAsync();

            var orderitemsupdated = await _dbContext.OrderItems
                            //.Where(u => u.Id == orderFormInputModel.Id && u.IsOrderConfirmed == false)
                            .Where(u => u.OrderId == orderFormInputModel.Id)
                            .ToListAsync();
                
                foreach (var item in orderitemsupdated)
                {
                    item.IsOrderItemConfirmed = true;
                }

            await _dbContext.SaveChangesAsync();

            var orderitems = await GetOrderItemsInOrderAsync(userId, orderFormInputModel.Id);

            var orderconfirmed = await _dbContext
                            .Orders
                            .Include(oi => oi.OrderUser)
                            .Include(oi => oi.Courier)
                            .Include(oi => oi.OrderItems)
                            .Where(u => u.Id == orderFormInputModel.Id && u.IsOrderConfirmed == true)
                            //.OrderByDescending(u => u.CreatedOn) // Optional: get latest if multiple exist
                            .Select(m => new OrderDetailsModel()
                            {
                                Id = m.Id,
                                OrderUserId = m.OrderUserId,
                                OrderUserName = m.OrderUser.UserName,
                                CourierId = m.CourierId,
                                CourierName = m.Courier.Name,
                                CreatedOnOrder = m.ConfirmedOn,
                                TotalCountProducts = m.OrderItems.Count(),
                                TotalPriceBGN = m.TotalPriceBGN,
                                IsOrderConfirmed = m.IsOrderConfirmed,
                                OrderItems = orderitems
                            })
                            .FirstOrDefaultAsync();

            return orderconfirmed;
        }


        public async Task<OrderFilterViewModel> ConfirmOrderHistoryAsync(string userId, DateOnly? startDate, DateOnly? endDate)
        {
            var orders = await _dbContext
                            .Orders
                            .Include(oi => oi.OrderUser)
                            .Include(oi => oi.Courier)
                            .Include(oi => oi.OrderItems)
                            .Where(u => (u.OrderUserId == userId || userId == "0") && u.IsOrderConfirmed == true & u.ConfirmedOn >= startDate & u.ConfirmedOn <= endDate)
                            .OrderByDescending(u => u.ConfirmedOn) // Optional: get latest if multiple exist
                            .ToListAsync();

            var orderConfirmed = new List<OrderDetailsModel>();

                        foreach (var m in orders)
                        {
                            orderConfirmed.Add(new OrderDetailsModel
                            {
                                Id = m.Id,
                                OrderUserId = m.OrderUserId,
                                OrderUserName = m.OrderUser.UserName,
                                CourierId = m.CourierId,
                                CourierName = m.Courier.Name,
                                CreatedOnOrder = m.ConfirmedOn,
                                TotalCountProducts = m.OrderItems.Count,
                                TotalPriceBGN = m.TotalPriceBGN,
                                IsOrderConfirmed = m.IsOrderConfirmed,
                                OrderItems = await GetOrderItemsInOrderAsync(userId, m.Id) // Note: use m.Id here
                            });
                        }

            var orderfilterviewmodel = new OrderFilterViewModel
            {
                StartDate = startDate,
                EndDate = endDate,
                Orders = orderConfirmed
            };

            return orderfilterviewmodel;
        }


        /*
        public async Task<IEnumerable<OrderItemDetailsModel>> GetOrderItemsInChoosenOrderAsync(string userId, int? OrderId)
        {
            var orderItems = await this._dbContext
                 .OrderItems
                 .Include(oi => oi.OrderItemAccessory)
                 .ThenInclude(r => r.Category)
                 .AsNoTracking()
                 .Where(m => m.OrderItemUserId == userId && m.OrderId == OrderId)
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
        */







    }
}
