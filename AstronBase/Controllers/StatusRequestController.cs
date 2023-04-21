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
    public class StatusRequestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatusRequestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StatusRequests
        public async Task<IActionResult> Index()
        {
              return _context.StatusRequest != null ? 
                          View(await _context.StatusRequest.ToListAsync()) :
                          Problem("Entity set 'AstronBaseContext.StatusRequest'  is null.");
        }

        // GET: StatusRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StatusRequest == null)
            {
                return NotFound();
            }

            var statusRequest = await _context.StatusRequest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statusRequest == null)
            {
                return NotFound();
            }

            return View(statusRequest);
        }

        // GET: StatusRequests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StatusRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] StatusRequest statusRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statusRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statusRequest);
        }

        // GET: StatusRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StatusRequest == null)
            {
                return NotFound();
            }

            var statusRequest = await _context.StatusRequest.FindAsync(id);
            if (statusRequest == null)
            {
                return NotFound();
            }
            return View(statusRequest);
        }

        // POST: StatusRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] StatusRequest statusRequest)
        {
            if (id != statusRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statusRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusRequestExists(statusRequest.Id))
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
            return View(statusRequest);
        }

        // GET: StatusRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StatusRequest == null)
            {
                return NotFound();
            }

            var statusRequest = await _context.StatusRequest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statusRequest == null)
            {
                return NotFound();
            }

            return View(statusRequest);
        }

        // POST: StatusRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StatusRequest == null)
            {
                return Problem("Entity set 'AstronBaseContext.StatusRequest'  is null.");
            }
            var statusRequest = await _context.StatusRequest.FindAsync(id);
            if (statusRequest != null)
            {
                _context.StatusRequest.Remove(statusRequest);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusRequestExists(int id)
        {
          return (_context.StatusRequest?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
