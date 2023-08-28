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
using AstronBase.Domain.ViewModels.StatusBlank;
using AstronBase.Service.Implementations;

namespace AstronBase.Controllers
{
    public class StatusBlankController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IStatusBlankService _statusBlankService;

        public StatusBlankController(ApplicationDbContext context, IStatusBlankService statusBlankService)
        {
            _db = context;
            _statusBlankService = statusBlankService;
        }

        public async Task<IActionResult> Index(string searchString, int page = 1)
        {

            ViewBag.CurrentFilter = searchString;
            var response = await _statusBlankService.GetStatusBlanks();

            if (!string.IsNullOrEmpty(searchString))
            {

                response = await _statusBlankService.GetStatusBlankBySearch(searchString);

            }

            const int pageSize = 5;

            var count = response.Data.Count();

            var items = response.Data.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var pageViewModel = new PageViewModel(count, page, pageSize);

            var viewModel = new StatusBlankIndexViewModel(items, pageViewModel);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _statusBlankService.GetStatusBlank(id);

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
        public async Task<IActionResult> Create(StatusBlankCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await _statusBlankService.CreateStatusBlank(model);

                    return RedirectToAction("Index");
                }

            }

            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (_statusBlankService.GetStatusBlank(id) == null)
            {
                return NotFound();
            }

            var response = await _statusBlankService.GetStatusBlank(id);

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
            var response = await _statusBlankService.GetStatusBlank(id);

            if (response == null)
            {
                return Problem("Entity set 'ApplicationDbContext.StatusBlank'  is null.");
            }

            if (response.Data != null)
            {
                await _statusBlankService.DeleteStatusBlank(id);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var statusBlank = await _statusBlankService.GetStatusBlank(id);

            return View(statusBlank.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] StatusBlankEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != 0)
                {
                    await _statusBlankService.Edit(model.Id, model);
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
