using AccessoriesApp.Data;
using AccessoriesApp.Data.Models;
using AccessoriesApp.Services;
using AccessoriesApp.Services.Interfaces;
using AccessoriesApp.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;




namespace AccessoriesApp.Web.Controllers
{

    [Authorize]
    public class FavoritesController : Controller
    {
        
        private readonly IFavoriteService _favoriteService;
                
        public FavoritesController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllFavorites()
        {
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var favorites = await _favoriteService.GetFavoriteAsync(userId);
            return View(favorites);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Save(int id, int categoryid)
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

            else if (referrer.Contains("/Accessories"))
            {
                if (Uri.TryCreate(referrer, UriKind.Absolute, out Uri uri))
                {
                    string[] segments = uri.Segments;
                    // segments: ["/", "Accessories/", "Index/", "1"]
                    if (segments.Length >= 4)
                    {
                        string idSegment = segments[3].TrimEnd('/');
                        if (int.TryParse(idSegment, out int idout))
                        {
                            // id now contains 1
                            // use it as needed
                            if (idout > 0)
                            {
                                return RedirectToAction(nameof(Index), "Accessories", new { id = categoryid });
                            }
                        }
                    }
                }
            }

            //return RedirectToAction(nameof(GetAllFavorites));
            return RedirectToAction(nameof(Index), "Accessories");
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Remove(int id, int categoryid)
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
            else if (referrer.Contains("/Favorites/GetAllFavorites"))
            {
                return RedirectToAction(nameof(FavoritesController.GetAllFavorites), "Favorites");
            }
            else if (referrer.Contains("/Accessories"))
            {
                if (Uri.TryCreate(referrer, UriKind.Absolute, out Uri uri))
                {
                    string[] segments = uri.Segments;
                    // segments: ["/", "Accessories/", "Index/", "1"]
                    if (segments.Length >= 4)
                    {
                        string idSegment = segments[3].TrimEnd('/');
                        if (int.TryParse(idSegment, out int idout))
                        {
                            // id now contains 1
                            // use it as needed
                            if (idout > 0)
                            {
                                return RedirectToAction(nameof(Index), "Accessories", new { id = categoryid });
                            }
                        }
                    }
                }
            }


            //return RedirectToAction(nameof(GetAllFavorites));
            return RedirectToAction(nameof(Index), "Accessories");
        }



    }
}
