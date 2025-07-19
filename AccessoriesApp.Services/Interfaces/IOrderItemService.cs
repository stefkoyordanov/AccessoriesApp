using AccessoriesApp.Web.ViewModels;

namespace AccessoriesApp.Services.Interfaces
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItemDetailsModel>> GetAllOrderItemAsync(string userId);
        Task<OrderItemFormInputModel> GetOrderItemFormInputByIdAsync(int id, string userId);
        Task<bool> AddToOrderItem_OrderAsync(OrderItemFormInputModel inputModel, string userId);
        Task<OrderItemFormInputModel?> GetEditableOrderItemByIdAsync(int id);
        Task<OrderItemResultModel> EditAccessoryAsync(OrderItemFormInputModel inputModel);
        Task<decimal> TotalSumOrder(int? orderid);

        Task<OrderItemDetailsModel?> GetOrderItemForDeleteAsync(int id, string userId);
        Task<int> RemoveFromOrderItemAsync(int id, string userId);
    }
}