using AccessoriesApp.Data;
using AccessoriesApp.Services;
using AccessoriesApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AccessoriesApp.Web.ViewModels;




namespace AccessoriesApp.Web.Controllers
{
    public class FavoritesController : Controller
    {
        
        private readonly IFavoriteService _favoriteService;

        public FavoritesController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllFavorites()
        {
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var favorites = await _favoriteService.GetFavoriteAsync(userId);
            return View(favorites);
        }

        [HttpPost]
        public async Task<IActionResult> Save(int id)
        {
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            bool success = await _favoriteService.AddToFavoritesAsync(id, userId);

            
            if (!success)
            {
                return NotFound();
            }
            


            string referrer = Request.Headers["Referer"].ToString();
            if (referrer.Contains("/Accessories/Details"))
            {
                return RedirectToAction(nameof(AccessoriesController.Details), "Accessories", new { id = id });
            }
            //return RedirectToAction(nameof(GetAllFavorites));
            return RedirectToAction(nameof(Index), "Accessories");
        }


        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            bool success = await _favoriteService.RemoveFromFavoritesAsync(id, userId);

            if (!success)
            {
                return NotFound();
            }

            string referrer = Request.Headers["Referer"].ToString();
            if (referrer.Contains("/Accessories/Details"))
            {
                return RedirectToAction(nameof(AccessoriesController.Details), "Accessories", new { id = id });
            }
            //return RedirectToAction(nameof(GetAllFavorites));
            return RedirectToAction(nameof(Index), "Accessories");
        }



    }
}
