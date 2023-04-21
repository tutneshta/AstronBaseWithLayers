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

namespace AstronBase.Controllers
{
    public class StatusFiscalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatusFiscalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StatusFiscals
        public async Task<IActionResult> Index()
        {
              return _context.StatusFiscal != null ? 
                          View(await _context.StatusFiscal.ToListAsync()) :
                          Problem("Entity set 'AstronBaseContext.StatusFiscal'  is null.");
        }

        // GET: StatusFiscals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StatusFiscal == null)
            {
                return NotFound();
            }

            var statusFiscal = await _context.StatusFiscal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statusFiscal == null)
            {
                return NotFound();
            }

            return View(statusFiscal);
        }

        // GET: StatusFiscals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StatusFiscals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] StatusFiscal statusFiscal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statusFiscal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statusFiscal);
        }

        // GET: StatusFiscals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StatusFiscal == null)
            {
                return NotFound();
            }

            var statusFiscal = await _context.StatusFiscal.FindAsync(id);
            if (statusFiscal == null)
            {
                return NotFound();
            }
            return View(statusFiscal);
        }

        // POST: StatusFiscals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] StatusFiscal statusFiscal)
        {
            if (id != statusFiscal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statusFiscal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusFiscalExists(statusFiscal.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(statusFiscal);
        }

        // GET: StatusFiscals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StatusFiscal == null)
            {
                return NotFound();
            }

            var statusFiscal = await _context.StatusFiscal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statusFiscal == null)
            {
                return NotFound();
            }

            return View(statusFiscal);
        }

        // POST: StatusFiscals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StatusFiscal == null)
            {
                return Problem("Entity set 'AstronBaseContext.StatusFiscal'  is null.");
            }
            var statusFiscal = await _context.StatusFiscal.FindAsync(id);
            if (statusFiscal != null)
            {
                _context.StatusFiscal.Remove(statusFiscal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusFiscalExists(int id)
        {
          return (_context.StatusFiscal?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
