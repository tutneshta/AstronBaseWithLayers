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
    public class StatusBlankController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatusBlankController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StatusBlanks
        public async Task<IActionResult> Index()
        {
              return _context.StatusBlank != null ? 
                          View(await _context.StatusBlank.ToListAsync()) :
                          Problem("Entity set 'AstronBaseContext.StatusBlank'  is null.");
        }

        // GET: StatusBlanks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StatusBlank == null)
            {
                return NotFound();
            }

            var statusBlank = await _context.StatusBlank
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statusBlank == null)
            {
                return NotFound();
            }

            return View(statusBlank);
        }

        // GET: StatusBlanks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StatusBlanks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] StatusBlank statusBlank)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statusBlank);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statusBlank);
        }

        // GET: StatusBlanks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StatusBlank == null)
            {
                return NotFound();
            }

            var statusBlank = await _context.StatusBlank.FindAsync(id);
            if (statusBlank == null)
            {
                return NotFound();
            }
            return View(statusBlank);
        }

        // POST: StatusBlanks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] StatusBlank statusBlank)
        {
            if (id != statusBlank.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statusBlank);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusBlankExists(statusBlank.Id))
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
            return View(statusBlank);
        }

        // GET: StatusBlanks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StatusBlank == null)
            {
                return NotFound();
            }

            var statusBlank = await _context.StatusBlank
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statusBlank == null)
            {
                return NotFound();
            }

            return View(statusBlank);
        }

        // POST: StatusBlanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StatusBlank == null)
            {
                return Problem("Entity set 'AstronBaseContext.StatusBlank'  is null.");
            }
            var statusBlank = await _context.StatusBlank.FindAsync(id);
            if (statusBlank != null)
            {
                _context.StatusBlank.Remove(statusBlank);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusBlankExists(int id)
        {
          return (_context.StatusBlank?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
