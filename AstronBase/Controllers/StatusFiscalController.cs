using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstronBase.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using AstronBase.Models;
using AstronBase.DAL;
using AstronBase.Service.Interfaces;
using AstronBase.Domain.ViewModels.Model;
using AstronBase.Domain.ViewModels.Pagination;
using AstronBase.Domain.ViewModels.StatusFiscal;
using AstronBase.Service.Implementations;

namespace AstronBase.Controllers
{
    public class StatusFiscalController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IStatusFiscalService _statusFiscalService;

        public StatusFiscalController(ApplicationDbContext db, IStatusFiscalService statusFiscalService)
        {
            _db = db;
            _statusFiscalService = statusFiscalService;
        }

        public async Task<IActionResult> Index(string searchString, int page = 1)
        {

            ViewBag.CurrentFilter = searchString;
            var response = await _statusFiscalService.GetStatusFiscals();

            if (!string.IsNullOrEmpty(searchString))
            {

                response = await _statusFiscalService.GetStatusFiscalBySearch(searchString);

            }

            const int pageSize = 5;

            var count = response.Data.Count();

            var items = response.Data.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var pageViewModel = new PageViewModel(count, page, pageSize);

            var viewModel = new StatusFiscalIndexViewModel(items, pageViewModel);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _statusFiscalService.GetStatusFiscal(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return Redirect("Error");
        }


        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StatusFiscalCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await _statusFiscalService.CreateStatusFiscal(model);

                    return RedirectToAction("Index");
                }

            }

            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (_statusFiscalService.GetStatusFiscal(id) == null)
            {
                return NotFound();
            }

            var response = await _statusFiscalService.GetStatusFiscal(id);

            if (response == null)
            {
                return NotFound();
            }

            return View(response.Data);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _statusFiscalService.GetStatusFiscal(id);

            if (response == null)
            {
                return Problem("Entity set 'Context.StatusFiscal'  is null.");
            }

            if (response.Data != null)
            {
                await _statusFiscalService.DeleteStatusFiscal(id);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var store = await _statusFiscalService.GetStatusFiscal(id);

            return View(store.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] StatusFiscalEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != 0)
                {
                    await _statusFiscalService.Edit(model.Id, model);
                }
                else
                {
                    return Redirect("Error");
                }

                return RedirectToAction("Index");
            }
            return Redirect("Error");

        }
    }
}
