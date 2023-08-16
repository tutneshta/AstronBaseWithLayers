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
using AstronBase.Domain.ViewModels.RegisterState;
using AstronBase.Service.Implementations;

namespace AstronBase.Controllers
{
    public class RegisterStateController : Controller
    {
        private readonly IRegisterStateService _registerStateService;
        private readonly ApplicationDbContext _db;

        public RegisterStateController(IRegisterStateService registerStateService, ApplicationDbContext db)
        {
            _registerStateService = registerStateService;
            _db = db;
        }

        public async Task<IActionResult> Index(string searchString, int page = 1)
        {

            ViewBag.CurrentFilter = searchString;
            var response = await _registerStateService.GetRegisterStates();

            if (!string.IsNullOrEmpty(searchString))
            {

                response = await _registerStateService.GetRegisterStateBySearch(searchString);

            }

            const int pageSize = 5;

            var count = response.Data.Count();

            var items = response.Data.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var pageViewModel = new PageViewModel(count, page, pageSize);

            var viewModel = new RegisterStateIndexViewModel(items, pageViewModel);

            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _registerStateService.GetRegisterState(id);

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
        public async Task<IActionResult> Create(RegisterStateCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await _registerStateService.CreateRegisterState(model);

                    return RedirectToAction("Index");
                }

            }

            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (_registerStateService.GetRegisterState(id) == null)
            {
                return NotFound();
            }

            var response = await _registerStateService.GetRegisterState(id);

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
            var response = await _registerStateService.GetRegisterState(id);

            if (response == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RegisterStateNotFound'  is null.");
            }

            if (response.Data != null)
            {
                await _registerStateService.DeleteRegisterState(id);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var registerState = await _registerStateService.GetRegisterState(id);

            return View(registerState.Data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] RegisterStateEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != 0)
                {
                    await _registerStateService.Edit(model.Id, model);
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
