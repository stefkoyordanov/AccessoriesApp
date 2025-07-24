using AccessoriesApp.Data;
using AccessoriesApp.Data.Models;
using AccessoriesApp.Services.Interfaces;
using AccessoriesApp.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using static AccessoriesApp.GCommon.ApplicationConstants;
using static AccessoriesApp.Web.ViewModels.ValidationMessages.AccessoriesMessages;



namespace AccessoriesApp.Web.Controllers
{

    
    public class AccessoriesController : Controller
    {
        private readonly IAccessoryService _accessoryService;

        public AccessoriesController(IAccessoryService accessoryService)
        {
            _accessoryService = accessoryService;
        }

        
        // GET: Accessories
        [HttpGet]
        public async Task<IActionResult> Index(int id, int page = 1, int pageSize = 8)
        {
            string? userId = User.Identity?.IsAuthenticated == true ? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value : null;

            var accessories = await _accessoryService.GetAllAccessoriesAsync(userId, id);

            var totalItems = accessories.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = accessories
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModel = new PagedViewModel<AccessoriesIndexViewModel>
            {
                Items = items,
                CurrentPage = page,
                TotalPages = totalPages
            };



            return View(viewModel);
        }

        // GET: Accessories/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                string? userId = User.Identity?.IsAuthenticated == true ? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value : null;

                AccessoriesDetailsViewModel? accessoryDetails = await this._accessoryService
                    .GetAccessoryDetailsByIdAsync(id, userId);
                if (accessoryDetails == null)
                {
                    // TODO: Custom 404 page
                    return this.RedirectToAction(nameof(Index));
                }

                return this.View(accessoryDetails);
            }
            catch (Exception e)
            {
                // TODO: Implement it with the ILogger
                // TODO: Add JS bars to indicate such errors
                Console.WriteLine(e.Message);

                return this.RedirectToAction(nameof(Index));
            }
        }



        [Authorize(Roles = "Admin")]
        // GET: Accessories/Create
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AccessoriesFormInputModel
            {
                Categories = await GetCategoriesSelectList(),
                ReleaseDate = DateTime.UtcNow.ToString(AppDateFormat),
            };
            return View(model);            
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        private async Task<IEnumerable<CategoryViewModel>> GetCategoriesSelectList()
        {
            var categories = await _accessoryService.GetAllCategoriesAsync();
            return categories;
        }

        [Authorize(Roles = "Admin")]
        // POST: Accessories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AccessoriesFormInputModel inputModel)
        {

            if (inputModel.File != null && inputModel.File.Length > 0)
            {
                MemoryStream memoryStream = new MemoryStream();
                await inputModel.File.CopyToAsync(memoryStream);

                inputModel.ImageFileName = inputModel.File.FileName;
                inputModel.TypeImage = inputModel.File.ContentType;
                inputModel.Image = memoryStream.ToArray();
            }

            if (!this.ModelState.IsValid)
            {
                inputModel.Categories = await GetCategoriesSelectList();
                return this.View(inputModel);
            }            

            try
            {
                //string userId = "";
                string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                
                bool? addResult = await this._accessoryService.AddAccessoryAsync(inputModel, userId);

                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                // TODO: Implement it with the ILogger
                Console.WriteLine(e.Message);

                this.ModelState.AddModelError(string.Empty, e.InnerException.Message);
                inputModel.Categories = await GetCategoriesSelectList();
                return this.View(inputModel);
            }
            
        }


        [Authorize(Roles = "Admin")]
        // GET: Accessories/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                AccessoriesFormInputModel? editableAccessory = await this._accessoryService
                    .GetEditableAccessoryByIdAsync(id);
                if (editableAccessory == null)
                {
                    // TODO: Custom 404 page
                    return this.RedirectToAction(nameof(Index));
                }

                editableAccessory.Categories = await GetCategoriesSelectList();

                return this.View(editableAccessory);
            }
            catch (Exception e)
            {
                // TODO: Implement it with the ILogger
                // TODO: Add JS bars to indicate such errors
                Console.WriteLine(e.Message);

                return this.RedirectToAction(nameof(Index));
            }
        }


        [Authorize(Roles = "Admin")]
        // POST: Accessories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AccessoriesFormInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            try
            {
                bool editSuccess = await this._accessoryService.EditAccessoryAsync(inputModel);
                if (!editSuccess)
                {
                    // TODO: Custom 404 page
                    return this.RedirectToAction(nameof(Index));
                }

                return this.RedirectToAction(nameof(Details), new { id = inputModel.Id });
            }
            catch (Exception e)
            {
                // TODO: Implement it with the ILogger
                // TODO: Add JS bars to indicate such errors
                Console.WriteLine(e.Message);

                return this.RedirectToAction(nameof(Index));
            }
        }


        //[Authorize(Roles = "Admin")]
        // GET: Accessories/Delete/5
        [HttpGet]
        public async Task<IActionResult> MyDelete(int id)
        {
            try
            {
                string? userId = User.Identity?.IsAuthenticated == true ? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value : null;

                AccessoriesDetailsViewModel? accessoryDetails = await this._accessoryService
                    .GetAccessoryDetailsByIdAsync(id, userId);
                if (accessoryDetails == null)
                {
                    // TODO: Custom 404 page
                    return this.RedirectToAction(nameof(Index));
                }

                return this.View(accessoryDetails);
            }
            catch (Exception e)
            {
                // TODO: Implement it with the ILogger
                // TODO: Add JS bars to indicate such errors
                Console.WriteLine(e.Message);

                return this.RedirectToAction(nameof(Index));
            }
        }


        //[Authorize(Roles = "Admin")]
        // POST: Accessories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            string? userId = User.Identity?.IsAuthenticated == true ? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value : null;

            AccessoriesDetailsViewModel? accessoryDetails = await this._accessoryService
                    .GetAccessoryDetailsByIdAsync(id, userId);
            if (accessoryDetails == null)
            {
                // TODO: Custom 404 page
                return this.RedirectToAction(nameof(Index));
            }
            await _accessoryService.DeleteAccessoryAsync(id);
            
            return RedirectToAction(nameof(Index));
        }


        


    }
}
