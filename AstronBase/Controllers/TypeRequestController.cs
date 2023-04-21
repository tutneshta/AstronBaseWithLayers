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
    public class TypeRequestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypeRequestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TypeRequests
        public async Task<IActionResult> Index()
        {
              return _context.TypeRequest != null ? 
                          View(await _context.TypeRequest.ToListAsync()) :
                          Problem("Entity set 'AstronBaseContext.TypeRequest'  is null.");
        }

        // GET: TypeRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TypeRequest == null)
            {
                return NotFound();
            }

            var typeRequest = await _context.TypeRequest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeRequest == null)
            {
                return NotFound();
            }

            return View(typeRequest);
        }

        // GET: TypeRequests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TypeRequest typeRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeRequest);
        }

        // GET: TypeRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TypeRequest == null)
            {
                return NotFound();
            }

            var typeRequest = await _context.TypeRequest.FindAsync(id);
            if (typeRequest == null)
            {
                return NotFound();
            }
            return View(typeRequest);
        }

        // POST: TypeRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TypeRequest typeRequest)
        {
            if (id != typeRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeRequestExists(typeRequest.Id))
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
            return View(typeRequest);
        }

        // GET: TypeRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TypeRequest == null)
            {
                return NotFound();
            }

            var typeRequest = await _context.TypeRequest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeRequest == null)
            {
                return NotFound();
            }

            return View(typeRequest);
        }

        // POST: TypeRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TypeRequest == null)
            {
                return Problem("Entity set 'AstronBaseContext.TypeRequest'  is null.");
            }
            var typeRequest = await _context.TypeRequest.FindAsync(id);
            if (typeRequest != null)
            {
                _context.TypeRequest.Remove(typeRequest);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeRequestExists(int id)
        {
          return (_context.TypeRequest?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
