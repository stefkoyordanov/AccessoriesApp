using AccessoriesApp.Data;
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

        public async Task<IEnumerable<OrderItemDetailsModel>> GetOrderItemsInOrderAsync(string userId)
        {
            var orderItems = await this._dbContext
                 .OrderItems
                 .Include(oi => oi.OrderItemAccessory)
                 .ThenInclude(r => r.Category)
                 .AsNoTracking()
                 .Where(m => m.OrderItemUserId == userId)
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
                        .Where(u => u.Id == orderid && u.IsOrderFulfilled == false)
                        .FirstOrDefaultAsync();

            return ordertotal.TotalPriceBGN;
        }







    }


}
