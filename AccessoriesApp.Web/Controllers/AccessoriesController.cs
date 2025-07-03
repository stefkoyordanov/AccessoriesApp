using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AccessoriesApp.Data;
using AccessoriesApp.Data.Models;
using AccessoriesApp.Services.Interfaces;
using AccessoriesApp.Web.ViewModels.Accessory;
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
        public async Task<IActionResult> Index()
        {
            return View(await _accessoryService.GetAllAccessoriesAsync());
        }

        // GET: Accessories/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            try
            {
                AccessoriesDetailsViewModel? accessoryDetails = await this._accessoryService
                    .GetAccessoryDetailsByIdAsync(id);
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

        // GET: Accessories/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Accessories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AccessoriesFormInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            try
            {
                await this._accessoryService.AddAccessoryAsync(inputModel);

                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                // TODO: Implement it with the ILogger
                Console.WriteLine(e.Message);

                this.ModelState.AddModelError(string.Empty, ServiceCreateError);
                return this.View(inputModel);
            }
        }

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

        // GET: Accessories/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            try
            {
                AccessoriesDetailsViewModel? accessoryDetails = await this._accessoryService
                    .GetAccessoryDetailsByIdAsync(id);
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

        // POST: Accessories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string? id)
        {
            AccessoriesDetailsViewModel? accessoryDetails = await this._accessoryService
                    .GetAccessoryDetailsByIdAsync(id);
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
