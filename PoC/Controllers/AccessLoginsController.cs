using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PoC.Context;
using PoC.Models;

namespace PoC.Controllers
{
    public class AccessLoginsController : Controller
    {
        private readonly ShareLoginDatabaseContext _context;

        public AccessLoginsController(ShareLoginDatabaseContext context)
        {
            _context = context;
        }

        // GET: AccessLogins
        public async Task<IActionResult> Index()
        {
              return _context.AccessLogin != null ? 
                          View(await _context.AccessLogin.ToListAsync()) :
                          Problem("Entity set 'ShareLoginDatabaseContext.AccessLogin'  is null.");
        }

        // GET: AccessLogins/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.AccessLogin == null)
            {
                return NotFound();
            }

            var accessLogin = await _context.AccessLogin
                .FirstOrDefaultAsync(m => m.IdAccessLogin == id);
            if (accessLogin == null)
            {
                return NotFound();
            }

            return View(accessLogin);
        }

        // GET: AccessLogins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AccessLogins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlataformLink,Password,numberUsers")] AccessLogin accessLogin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accessLogin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accessLogin);
        }

        // GET: AccessLogins/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.AccessLogin == null)
            {
                return NotFound();
            }

            var accessLogin = await _context.AccessLogin.FindAsync(id);
            if (accessLogin == null)
            {
                return NotFound();
            }
           
            return View(accessLogin);
        }

        // POST: AccessLogins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAccessLogin,PlataformLink,Password,UserPlatform")] AccessLogin accessLogin)
        {
            accessLogin.IdAccessLogin = id;
            if (id != accessLogin.IdAccessLogin)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accessLogin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccessLoginExists(accessLogin.PlataformLink))
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
            return View(accessLogin);
        }

        // GET: AccessLogins/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.AccessLogin == null)
            {
                return NotFound();
            }

            var accessLogin = await _context.AccessLogin
                .FirstOrDefaultAsync(m => m.PlataformLink == id);
            if (accessLogin == null)
            {
                return NotFound();
            }

            return View(accessLogin);
        }

        // POST: AccessLogins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.AccessLogin == null)
            {
                return Problem("Entity set 'ShareLoginDatabaseContext.AccessLogin'  is null.");
            }
            var accessLogin = await _context.AccessLogin.FindAsync(id);
            if (accessLogin != null)
            {
                _context.AccessLogin.Remove(accessLogin);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccessLoginExists(string id)
        {
          return (_context.AccessLogin?.Any(e => e.PlataformLink == id)).GetValueOrDefault();
        }
    }
}
