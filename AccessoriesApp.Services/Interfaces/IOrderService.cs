using AccessoriesApp.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessoriesApp.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderItemDetailsModel>> GetOrderItemsInOrderAsync(string userId);
        Task<IEnumerable<CourierViewModel>> GetAllCouriersAsync();
        Task<decimal> TotalSumOrder(int? orderid);
        Task<OrderDetailsModel> ConfirmOrderAsync(int id, string userId);
    }
}
