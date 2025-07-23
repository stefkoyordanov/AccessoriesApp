using AccessoriesApp.Services;
using AccessoriesApp.Services.Interfaces;
using AccessoriesApp.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AccessoriesApp.Web.Controllers
{
    public class OrderController : Controller
    {

        private readonly IOrderService _orderService;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderController(IOrderService orderService, UserManager<IdentityUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }
                
        [HttpGet]
        public async Task<IActionResult> Confirm(int id)
        {
            string? userId = User.Identity?.IsAuthenticated == true ? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value : null;
            var items = await _orderService.GetOrderItemsInOrderAsync(userId, id);

            var model = new OrderFormInputModel
            {
                Id = items.FirstOrDefault().OrderId,
                OrderItems = items,
                Couriers = await _orderService.GetAllCouriersAsync()
            };

            ViewData["SumOrder"] = await _orderService.TotalSumOrder(items.FirstOrDefault().OrderId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Confirmed(OrderFormInputModel confirmmodel)
        {
            /*
            if (!ModelState.IsValid)
            {                
                return View(model);
            }
            */

            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var orderconfirmed = await _orderService.ConfirmOrderAsync(confirmmodel, userId);

            return View(orderconfirmed);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmedHistory(DateOnly? startDate, DateOnly? endDate)
        {
            /*
            if (!ModelState.IsValid)
            {                
                return View(model);
            }
            */

            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var orderconfirmed = await _orderService.ConfirmOrderHistoryAsync(userId, startDate, endDate);

            return View(orderconfirmed);
        }





    }
}
