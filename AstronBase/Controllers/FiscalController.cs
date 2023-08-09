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
using AstronBase.Domain.ViewModels.Fiscal;
using AstronBase.Service.Interfaces;
using AstronBase.Domain.ViewModels.Pagination;
using AstronBase.Domain.ViewModels.Store;
using AstronBase.Service.Implementations;

namespace AstronBase.Controllers
{
    public class FiscalController : Controller
    {
        private readonly IFiscalService _fiscalService;
        private readonly ApplicationDbContext _db;

        public FiscalController(IFiscalService fiscalService, ApplicationDbContext db)
        {
            _fiscalService = fiscalService;
            _db = db;
        }

        private void CompaniesDropDownList(object selectedCompany = null)
        {
            var companyQuery = from d in _db.Company
                orderby d.Name
                select d;

            ViewBag.CompanyId = new SelectList(companyQuery, "Id", "Name", selectedCompany);
        }

        private void ModelFrDropDownList(object selectedModelFr = null)
        {
            var modelFrQuery = from d in _db.Model
                orderby d.Name
                select d;

            ViewBag.ModelId = new SelectList(modelFrQuery, "Id", "Name", selectedModelFr);
        }

        private void StatusFrDropDownList(object selectedStatusFr = null)
        {
            var statusFrQuery = from d in _db.StatusFiscal
                orderby d.Name
                select d;

            ViewBag.StatusFiscalId = new SelectList(statusFrQuery, "Id", "Name", selectedStatusFr);
        }

        private void StoresDropDownList(object selectedStores = null)
        {
            var storesFrQuery = from d in _db.Store
                orderby d.Name
                select d;

            ViewBag.StoreId = new SelectList(storesFrQuery, "Id", "Name", selectedStores);
        }

        private void RegisterStateDropDownList(object selectedRegisterState = null)
        {
            var registerStateFrQuery = from d in _db.RegisterState
                orderby d.Name
                select d;

            ViewBag.RegisterStateId = new SelectList(registerStateFrQuery
                , "Id", "Name", selectedRegisterState);
        }

        private void EngineerDropDownList(object selectedEngineer = null)
        {
            var engineerQuery = from d in _db.Engineer
                orderby d.LastName
                select d;

            ViewBag.EngineerId = new SelectList(engineerQuery
                , "Id", "LastName", selectedEngineer);
        }

        public async Task<IActionResult> Index(string searchString, int page = 1)
        {

            ViewBag.CurrentFilter = searchString;
            var response = await _fiscalService.GetFiscals();

            if (!string.IsNullOrEmpty(searchString))
            {

                response = await _fiscalService.GetFiscalBySearch(searchString);

            }

            const int pageSize = 5;

            var count = response.Data.Count();

            var items = response.Data.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var pageViewModel = new PageViewModel(count, page, pageSize);

            var viewModel = new FiscalIndexViewModel(items, pageViewModel);

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _fiscalService.GetFiscal(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return Redirect("Error");
        }

        [HttpGet]
        public IActionResult Create()
        {
            CompaniesDropDownList();
            StoresDropDownList();
            EngineerDropDownList();
            StatusFrDropDownList();
            ModelFrDropDownList();
            RegisterStateDropDownList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FiscalCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await _fiscalService.CreateFiscal(model);

                    return RedirectToAction("Index");
                }

            }

            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (_fiscalService.GetFiscal(id) == null)
            {
                return NotFound();
            }

            var response = await _fiscalService.GetFiscal(id);

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
            var response = await _fiscalService.GetFiscal(id);

            if (response == null)
            {
                return Problem("Entity set 'AstronBaseContext.Fiscal'  is null.");
            }

            if (response.Data != null)
            {
                await _fiscalService.DeleteFiscal(id);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var fiscal = await _fiscalService.GetFiscal(id);

            CompaniesDropDownList(fiscal.Data.CompanyId);
            StoresDropDownList(fiscal.Data.StoreId);
            EngineerDropDownList(fiscal.Data.EngineerId);
            StatusFrDropDownList(fiscal.Data.StatusFiscalId);
            ModelFrDropDownList(fiscal.Data.ModelId);
            RegisterStateDropDownList(fiscal.Data.RegisterStateId);

            return View(fiscal.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name, CompanyId, StoreId, EngineerId, StatusFiscalId, ModelId, RegisterStateId")] FiscalEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != 0)
                {
                    await _fiscalService.Edit(model.Id, model);
                }
                else
                {
                    return Redirect("Error");
                }

                return RedirectToAction("Index");
            }
            return Redirect("Error");


           // CompaniesDropDownList(model.CompanyId);
        }
    }
}