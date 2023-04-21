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
    public class RegisterStateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegisterStateController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RegisterStates
        public async Task<IActionResult> Index()
        {
              return _context.RegisterState != null ? 
                          View(await _context.RegisterState.ToListAsync()) :
                          Problem("Entity set 'AstronBaseContext.RegisterState'  is null.");
        }

        // GET: RegisterStates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RegisterState == null)
            {
                return NotFound();
            }

            var registerState = await _context.RegisterState
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registerState == null)
            {
                return NotFound();
            }

            return View(registerState);
        }

        // GET: RegisterStates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RegisterStates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] RegisterState registerState)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registerState);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(registerState);
        }

        // GET: RegisterStates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RegisterState == null)
            {
                return NotFound();
            }

            var registerState = await _context.RegisterState.FindAsync(id);
            if (registerState == null)
            {
                return NotFound();
            }
            return View(registerState);
        }

        // POST: RegisterStates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] RegisterState registerState)
        {
            if (id != registerState.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registerState);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegisterStateExists(registerState.Id))
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
            return View(registerState);
        }

        // GET: RegisterStates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RegisterState == null)
            {
                return NotFound();
            }

            var registerState = await _context.RegisterState
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registerState == null)
            {
                return NotFound();
            }

            return View(registerState);
        }

        // POST: RegisterStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RegisterState == null)
            {
                return Problem("Entity set 'AstronBaseContext.RegisterState'  is null.");
            }
            var registerState = await _context.RegisterState.FindAsync(id);
            if (registerState != null)
            {
                _context.RegisterState.Remove(registerState);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegisterStateExists(int id)
        {
          return (_context.RegisterState?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
