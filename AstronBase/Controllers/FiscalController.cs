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

namespace AstronBase.Controllers
{
    public class FiscalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FiscalController(ApplicationDbContext context)
        {
            _context = context;
        }

        private void CompaniesDropDownList(object selectedCompany = null)
        {
            var companyQuery = from d in _context.Company
                orderby d.Name
                select d;

            ViewBag.CompanyId = new SelectList(companyQuery, "Id", "Name", selectedCompany);
        }

        private void ModelFrDropDownList(object selectedModelFr = null)
        {
            var modelFrQuery = from d in _context.Model
                orderby d.Name
                select d;

            ViewBag.ModelId = new SelectList(modelFrQuery, "Id", "Name", selectedModelFr);
        }

        private void StatusFrDropDownList(object selectedStatusFr = null)
        {
            var statusFrQuery = from d in _context.StatusFiscal
                orderby d.Name
                select d;

            ViewBag.StatusFiscalId = new SelectList(statusFrQuery, "Id", "Name", selectedStatusFr);
        }

        private void StoresDropDownList(object selectedStores = null)
        {
            var storesFrQuery = from d in _context.Store
                orderby d.Name
                select d;

            ViewBag.StoreId = new SelectList(storesFrQuery, "Id", "Name", selectedStores);
        }

        private void RegisterStateDropDownList(object selectedRegisterState = null)
        {
            var registerStateFrQuery = from d in _context.RegisterState
                orderby d.Name
                select d;

            ViewBag.RegisterStateId = new SelectList(registerStateFrQuery
                , "Id", "Name", selectedRegisterState);
        }

        private void EngineerDropDownList(object selectedEngineer = null)
        {
            var engineerQuery = from d in _context.Engineer
                orderby d.LastName
                select d;

            ViewBag.EngineerId = new SelectList(engineerQuery
                , "Id", "LastName", selectedEngineer);
        }

        // GET: Fiscals
        public async Task<IActionResult> Index()
        {
            return _context.Fiscal != null
                ? View(await _context.Fiscal
                    .Include(c => c.Company)
                    .Include(s => s.Store)
                    .Include(c => c.Model)
                    .Include(c => c.StatusFiscal)
                    .Include(c => c.Engineer)
                    .Include(c => c.RegisterState)
                    .ToListAsync())
                : Problem("Entity set 'AstronBaseContext.Fiscal'  is null.");
        }

        // GET: Fiscals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var fiscal = await _context.Fiscal
                .Include(c => c.Company)
                .Include(c => c.Store)
                .Include(c => c.StatusFiscal)
                .Include(c => c.Model)
                .Include(c => c.Engineer)
                .Include(c => c.RegisterState)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (fiscal == null)
            {
                return NotFound();
            }

            return View(fiscal);
        }

        // GET: Fiscals/Create
        public IActionResult Create()
        {
            CompaniesDropDownList();
            ModelFrDropDownList();
            StatusFrDropDownList();
            StoresDropDownList();
            RegisterStateDropDownList();
            EngineerDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,SerialNumber,Note, StoreId, CompanyId, ModelId, RegisterStateId, StatusFiscalId, EngineerId ")]
            Fiscal fiscal)
        {
            _context.Add(fiscal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            return View(fiscal);
        }

        // GET: Fiscals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var fiscal = await _context.Fiscal.FindAsync(id);
            CompaniesDropDownList(fiscal.CompanyId);
            ModelFrDropDownList(fiscal.ModelId);
            StatusFrDropDownList(fiscal.StatusFiscalId);
            StoresDropDownList(fiscal.StoreId);
            RegisterStateDropDownList(fiscal.RegisterStateId);
            EngineerDropDownList(fiscal.EngineerId);

            return View(fiscal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SerialNumber,Note")] Fiscal fiscal)
        {
            try
            {
                _context.Update(fiscal);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FiscalExists(fiscal.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));

            CompaniesDropDownList(fiscal.CompanyId);
            ModelFrDropDownList(fiscal.ModelId);
            StatusFrDropDownList(fiscal.StatusFiscalId);
            StoresDropDownList(fiscal.StoreId);
            RegisterStateDropDownList(fiscal.RegisterStateId);
            EngineerDropDownList(fiscal.EngineerId);

            return View(fiscal);
        }

        // GET: Fiscals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fiscal == null)
            {
                return NotFound();
            }

            var fiscal = await _context.Fiscal
                .FirstOrDefaultAsync(m => m.Id == id);

            if (fiscal == null)
            {
                return NotFound();
            }

            return View(fiscal);
        }

        // POST: Fiscals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fiscal == null)
            {
                return Problem("Entity set 'AstronBaseContext.Fiscal'  is null.");
            }

            var fiscal = await _context.Fiscal.FindAsync(id);

            if (fiscal != null)
            {
                _context.Fiscal.Remove(fiscal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FiscalExists(int id)
        {
            return (_context.Fiscal?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}