using AccessoriesApp.Data;
using AccessoriesApp.Services;
using AccessoriesApp.Services.Interfaces;
using AccessoriesApp.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AccessoriesApp.Web.Controllers
{
    public class OrderItemController : Controller
    {
        private readonly IOrderItemService _orderItemService;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderItemController(IOrderItemService orderItemService, UserManager<IdentityUser> userManager)
        {
            _orderItemService = orderItemService;
            _userManager = userManager;
        }

        
        // GET: /OrderItem
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string? userId = User.Identity?.IsAuthenticated == true ? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value : null;
            var items = await _orderItemService.GetAllOrderItemAsync(userId);
            return View(items);
        }


        /*
        // GET: /OrderItems/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var item = await _orderItemService.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        */


        // GET: /OrderItems/Create
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            string? userId = User.Identity?.IsAuthenticated == true ? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value : null;
            var model = await _orderItemService.GetOrderItemFormInputByIdAsync(id, userId);

            return View(model);            
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderItemFormInputModel model)
        {
            /*
            if (!ModelState.IsValid)
            {                
                return View(model);
            }
            */

            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            await _orderItemService.AddToOrderItem_OrderAsync(model, userId);

            return RedirectToAction(nameof(Index));
        }



        // GET: /OrderItems/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _orderItemService.GetEditableOrderItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(OrderItemFormInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            await _orderItemService.EditAccessoryAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> EditFromBody([FromBody] OrderItemFormInputModel model)
        {
            /*
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            */

            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            OrderItemResultModel retval = await _orderItemService.EditAccessoryAsync(model);

            // Handle quantity logic (e.g. save or calculate)
            return Json(new { mytotalbgn = retval.totalpricebgn });
            
        }


        // GET: /OrderItems/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var item = await _orderItemService.GetOrderItemForDeleteAsync(id, userId);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }


        // POST: Accessories/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            OrderItemDetailsModel? accessoryDetails = await this._orderItemService
                    .GetOrderItemForDeleteAsync(id, userId);
            if (accessoryDetails == null)
            {
                // TODO: Custom 404 page
                return this.RedirectToAction(nameof(Index));
            }
            await _orderItemService.RemoveFromOrderItemAsync(id, userId);

            return RedirectToAction(nameof(Index));
        }






    }

}
