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
    public class EngineerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EngineerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Engineers
        public async Task<IActionResult> Index()
        {
              return _context.Engineer != null ? 
                          View(await _context.Engineer.ToListAsync()) :
                          Problem("Entity set 'AstronBaseContext.Engineer'  is null.");
        }

        // GET: Engineers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Engineer == null)
            {
                return NotFound();
            }

            var engineer = await _context.Engineer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (engineer == null)
            {
                return NotFound();
            }

            return View(engineer);
        }

        // GET: Engineers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Engineers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,PhoneNumber,Email")] Engineer engineer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(engineer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(engineer);
        }

        // GET: Engineers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Engineer == null)
            {
                return NotFound();
            }

            var engineer = await _context.Engineer.FindAsync(id);
            if (engineer == null)
            {
                return NotFound();
            }
            return View(engineer);
        }

        // POST: Engineers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,PhoneNumber,Email")] Engineer engineer)
        {
            if (id != engineer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(engineer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EngineerExists(engineer.Id))
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
            return View(engineer);
        }

        // GET: Engineers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Engineer == null)
            {
                return NotFound();
            }

            var engineer = await _context.Engineer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (engineer == null)
            {
                return NotFound();
            }

            return View(engineer);
        }

        // POST: Engineers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Engineer == null)
            {
                return Problem("Entity set 'AstronBaseContext.Engineer'  is null.");
            }
            var engineer = await _context.Engineer.FindAsync(id);
            if (engineer != null)
            {
                _context.Engineer.Remove(engineer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EngineerExists(int id)
        {
          return (_context.Engineer?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
