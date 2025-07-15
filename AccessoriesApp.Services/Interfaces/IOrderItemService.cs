using AccessoriesApp.Web.ViewModels;

namespace AccessoriesApp.Services.Interfaces
{
    public interface IOrderItemService
    {
        Task<OrderItemFormInputModel> GetOrderItemFormInputByIdAsync(int id, string userId);
        Task<bool> AddToOrderItemAsync(OrderItemFormInputModel inputModel, string userId);
        Task<OrderItemFormInputModel?> GetEditableOrderItemByIdAsync(int id);
        Task<bool> EditAccessoryAsync(OrderItemFormInputModel inputModel);        
        
        Task<OrderItemDetailsModel?> GetRecipeForDeleteAsync(int id, string userId);
        Task<int> RemoveFromOrderItemAsync(int id, string userId);
    }
}