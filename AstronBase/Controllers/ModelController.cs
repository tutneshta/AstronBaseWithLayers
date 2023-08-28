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
using AstronBase.Domain.ViewModels.Model;
using AstronBase.Service.Interfaces;
using AstronBase.Domain.ViewModels.Pagination;
using AstronBase.Domain.ViewModels.Store;
using AstronBase.Service.Implementations;

namespace AstronBase.Controllers
{
    public class ModelController : Controller
    {
        private readonly IModelService _modelService;
        private readonly ApplicationDbContext _db;

        public ModelController(IModelService modelService, ApplicationDbContext db)
        {
            _modelService = modelService;
            _db = db;
        }

        public async Task<IActionResult> Index(string searchString, int page = 1)
        {

            ViewBag.CurrentFilter = searchString;
            var response = await _modelService.GetModels();

            if (!string.IsNullOrEmpty(searchString))
            {

                response = await _modelService.GetModelBySearch(searchString);

            }

            const int pageSize = 5;

            var count = response.Data.Count();

            var items = response.Data.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var pageViewModel = new PageViewModel(count, page, pageSize);

            var viewModel = new ModelIndexViewModel(items, pageViewModel);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _modelService.GetModel(id);

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
        public async Task<IActionResult> Create(ModelCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await _modelService.CreateModel(model);

                    return RedirectToAction("Index");
                }

            }

            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (_modelService.GetModel(id) == null)
            {
                return NotFound();
            }

            var response = await _modelService.GetModel(id);

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
            var response = await _modelService.GetModel(id);

            if (response == null)
            {
                return Problem("Entity set 'Context.Model'  is null.");
            }

            if (response.Data != null)
            {
                await _modelService.DeleteModel(id);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var store = await _modelService.GetModel(id);

            return View(store.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ModelEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != 0)
                {
                    await _modelService.Edit(model.Id, model);
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
