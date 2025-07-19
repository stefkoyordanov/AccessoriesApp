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

        // GET: /OrderItem
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string? userId = User.Identity?.IsAuthenticated == true ? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value : null;
            var items = await _orderService.GetOrderItemsInOrderAsync(userId);

            var model = new OrderFormInputModel
            {
                OrderItems = items,
                ReleaseDate = DateTime.UtcNow.ToString(AppDateFormat),
            };
            return View(model);

            ViewData["SumOrder"] = await _orderService.TotalSumOrder(items.FirstOrDefault().OrderId);
            return View(items);
        }

    }
}
