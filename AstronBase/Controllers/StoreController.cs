using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using AstronBase.Models;
using System.Data;
using AstronBase.DAL;
using AstronBase.Domain.ViewModels.Store;
using AstronBase.Service.Interfaces;

using static System.Runtime.InteropServices.JavaScript.JSType;


namespace AstronBase.Controllers
{
    public class StoreController : Controller
    {

        private readonly IStoreService _storeService;
        private readonly ApplicationDbContext _db;

        public StoreController(IStoreService storeService, ApplicationDbContext db)
        {
            _storeService = storeService;
            _db = db;
        }

        /// <summary>
        /// request to withdraw all stores
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _storeService.GetStores();

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return Redirect("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _storeService.GetStore(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return Redirect("Error");
        }

        private void CompaniesDropDownList(object selectedCompany = null)
        {
            var companyQuery = from d in _db.Company
                               orderby d.Name
                               select d;

            ViewBag.CompanyId = new SelectList(companyQuery, "Id", "Name", selectedCompany);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CompaniesDropDownList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StoreViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await _storeService.CreateStore(model);
                
                    return RedirectToAction("Index");
                }

                await _storeService.Edit(model.Id, model);
            }

            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (_storeService.GetStore(id) == null)
            {
                return NotFound();
            }

            var response = await _storeService.GetStore(id);

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
            var response = await _storeService.GetStore(id);

            if (response == null)
            {
                return Problem("Entity set 'AstronBaseContext.Store'  is null.");
            }

            if (response.Data != null)
            {
                await _storeService.DeleteStore(id);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var store = await _storeService.GetStore(id);

            CompaniesDropDownList(store.Data.CompanyId);

            return View(store.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name, CompanyId")] StoreViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await _storeService.Edit(model.Id, model);
                }
                else
                {
                    await _storeService.Edit(model.Id, model);
                }

                return RedirectToAction("Index");
            }

            return Redirect("Error");

            CompaniesDropDownList(model.CompanyId);
        }


        //public JsonResult GetSearchValue(string search)
        //{
        //    var allsearch = _context.Company
        //        .Where(x => x.Name.Contains(search)).Select(x => new Company()
        //        {
        //            Id = x.Id,
        //            Name = x.Name
        //        }).ToList();

        //    return Json(allsearch, new System.Text.Json.JsonSerializerOptions());
        //}
    }
}