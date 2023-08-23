using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AstronBase.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using AstronBase.Models;
using AstronBase.DAL;
using AstronBase.Service.Interfaces;
using AstronBase.Domain.ViewModels.Client;
using AstronBase.Domain.ViewModels.Pagination;
using AstronBase.Domain.ViewModels.Request;
using AstronBase.Service.Implementations;
using Microsoft.VisualBasic.CompilerServices;

namespace AstronBase.Controllers
{
    public class RequestController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IRequestService _requestService;

        public RequestController(ApplicationDbContext db, IRequestService requestService)
        {
            _db = db;
            _requestService = requestService;
        }

        private void CompaniesDropDownList(object selectedCompany = null)
        {
            var companyQuery = from d in _db.Company
                orderby d.Name
                select d;

            ViewBag.CompanyId = new SelectList(companyQuery, "Id", "Name", selectedCompany);
        }

        private void StoresDropDownList(object selectedStore = null)
        {
            var storeQuery = from d in _db.Store
                orderby d.Name
                select d;

            ViewBag.StoreId = new SelectList(storeQuery, "Id", "Name", selectedStore);
        }

        private void ClientDropDownList(object selectedClient = null)
        {
            var clientQuery = from d in _db.Client
                orderby d.Name
                select d;

            ViewBag.ClientId = new SelectList(clientQuery, "Id", "Name", selectedClient);
        }

        private void FiscalDropDownList(object selectedFiscal = null)
        {
            var fiscalQuery = from d in _db.Fiscal
                orderby d.SerialNumber
                select d;

            ViewBag.FiscalId = new SelectList(fiscalQuery, "Id", "SerialNumber", selectedFiscal);
        }

        private void EngineerDropDownList(object selectedEngineer = null)
        {
            var engineerQuery = from d in _db.Engineer
                orderby d.LastName
                select d;

            ViewBag.EngineerId = new SelectList(engineerQuery, "Id", "LastName", selectedEngineer);
        }

        private void StatusRequestDropDownList(object selectedStatusRequest = null)
        {
            var statusRequestQuery = from d in _db.StatusRequest
                orderby d.Name
                select d;

            ViewBag.StatusRequestId = new SelectList(statusRequestQuery, "Id", "Name", selectedStatusRequest);
        }

        private void TypeRequestDropDownList(object selectedTypeRequest = null)
        {
            var typeRequestQuery = from d in _db.TypeRequest
                orderby d.Name
                select d;

            ViewBag.TypeRequestId = new SelectList(typeRequestQuery, "Id", "Name", selectedTypeRequest);
        }

        private void StatusBlankDropDownList(object selectedStatusBlank = null)
        {
            var statusBlankQuery = from d in _db.StatusBlank
                                   orderby d.Name
                select d;

            ViewBag.StatusBlankId = new SelectList(statusBlankQuery, "Id", "Name", selectedStatusBlank);
        }

        public async Task<IActionResult> Index(string number, int page = 1)
        {

            ViewBag.CurrentFilter = number;
            var response = await _requestService.GetRequests();

            if (number != null)
            {

                response = await _requestService.GetRequestBySearch(number);

            }

            const int pageSize = 5;

            var count = response.Data.Count();

            var items = response.Data.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var pageViewModel = new PageViewModel(count, page, pageSize);

            var viewModel = new RequestIndexViewModel(items, pageViewModel);

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _requestService.GetRequest(id);

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
            ClientDropDownList();
            EngineerDropDownList();
            StatusBlankDropDownList();
            StatusRequestDropDownList();
            FiscalDropDownList();
            TypeRequestDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RequestCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await _requestService.CreateRequest(model);

                    return RedirectToAction("Index");
                }

            }

            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (_requestService.GetRequest(id) == null)
            {
                return NotFound();
            }

            var response = await _requestService.GetRequest(id);

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
            var response = await _requestService.GetRequest(id);

            if (response == null)
            {
                return Problem("Entity set 'Context.Request'  is null.");
            }

            if (response.Data != null)
            {
                await _requestService.DeleteRequest(id);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _requestService.GetRequest(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return NotFound();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RequestEditViewModel model)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction("Index");
            }

            return Redirect("Error");
        }
    }
}