using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using AstronBase.Domain.Entity;
using AstronBase.Models;
using AstronBase.DAL;
using AstronBase.Domain.ViewModels.Engineer;
using AstronBase.Service.Interfaces;
using AstronBase.Domain.ViewModels.Pagination;
using AstronBase.Domain.ViewModels.Store;
using AstronBase.Service.Implementations;

namespace AstronBase.Controllers
{
    public class EngineerController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IEngineerService _engineerService;

        public EngineerController(ApplicationDbContext context, IEngineerService engineerService)
        {
            _db = context;
            _engineerService = engineerService;
        }

        public async Task<IActionResult> Index(string searchString, int page = 1)
        {

            ViewBag.CurrentFilter = searchString;
            var response = await _engineerService.GetEngineers();

            if (!string.IsNullOrEmpty(searchString))
            {

                response = await _engineerService.GetEngineerBySearch(searchString);

            }

            const int pageSize = 5;

            var count = response.Data.Count();

            var items = response.Data.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var pageViewModel = new PageViewModel(count, page, pageSize);

            var viewModel = new EngineerIndexViewModel(items, pageViewModel);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _engineerService.GetEngineer(id);

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
        public async Task<IActionResult> Create(EngineerCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await _engineerService.CreateEngineer(model);

                    return RedirectToAction("Index");
                }

            }

            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (_engineerService.GetEngineer(id) == null)
            {
                return NotFound();
            }

            var response = await _engineerService.GetEngineer(id);

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
            var response = await _engineerService.GetEngineer(id);

            if (response == null)
            {
                return Problem("Entity set 'AstronBaseContext.Engineer'  is null.");
            }

            if (response.Data != null)
            {
                await _engineerService.DeleteEngineer(id);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var store = await _engineerService.GetEngineer(id);

            return View(store.Data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EngineerEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != 0)
                {
                    await _engineerService.Edit(model.Id, model);
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
