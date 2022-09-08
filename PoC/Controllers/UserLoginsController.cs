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
    public class UserLoginsController : Controller
    {
        private readonly ShareLoginDatabaseContext _context;

        public UserLoginsController(ShareLoginDatabaseContext context)
        {
            _context = context;
        }

        // GET: UserLogins
        public async Task<IActionResult> Index()
        {
            var shareLoginDatabaseContext = _context.UserLogin.Include(u => u.AccessLogin).Include(u => u.User);
            return View(await shareLoginDatabaseContext.ToListAsync());
        }

        // GET: UserLogins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserLogin == null)
            {
                return NotFound();
            }

            var userLogin = await _context.UserLogin
                .Include(u => u.AccessLogin)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.IdUser == id);
            if (userLogin == null)
            {
                return NotFound();
            }

            return View(userLogin);
        }

        // GET: UserLogins/Create
        public IActionResult Create()
        {
            ViewData["IdAccessLogin"] = new SelectList(_context.AccessLogin, "IdAccessLogin", "IdAccessLogin");
            ViewData["IdUser"] = new SelectList(_context.User, "IdUser", "IdUser");
            
            var userLogin = new UserLogin();
            userLogin.Logins = _context.AccessLogin.Select(x => 
                    new AccessLogin
                    {
                        IdAccessLogin = x.IdAccessLogin,
                        PlataformLink = x.PlataformLink
                    }
                ).ToList();
            //userLogin.Logins = new List<AccessLogin>
            //    {
            //        new AccessLogin { IdAccessLogin = 1, PlataformLink = "Jutube" },
            //        new AccessLogin { IdAccessLogin = 2, PlataformLink = "ErNetflix" }
            //    };
            return View(userLogin);
        }

        // POST: UserLogins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int Id,[Bind("IdAccessLogin,IdUser")]UserLogin userLogin)
        {
            userLogin.IdUser = Id;
            if (ModelState.IsValid)
            {
                _context.Add(userLogin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAccessLogin"] = new SelectList(_context.AccessLogin, "IdAccessLogin", "IdAccessLogin", userLogin.IdAccessLogin);
            ViewData["IdUser"] = new SelectList(_context.User, "IdUser", "IdUser", userLogin.IdUser);
            userLogin.Logins = _context.AccessLogin.Select(x =>
                    new AccessLogin
                    {
                        IdAccessLogin = x.IdAccessLogin,
                        PlataformLink = x.PlataformLink
                    }
                ).ToList();
            return View(userLogin);
        }

        // GET: UserLogins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserLogin == null)
            {
                return NotFound();
            }

            var userLogin = await _context.UserLogin.FindAsync(id);
            if (userLogin == null)
            {
                return NotFound();
            }
            ViewData["IdAccessLogin"] = new SelectList(_context.AccessLogin, "IdAccessLogin", "IdAccessLogin", userLogin.IdAccessLogin);
            ViewData["IdUser"] = new SelectList(_context.User, "IdUser", "IdUser", userLogin.IdUser);
            return View(userLogin);
        }

        // POST: UserLogins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUser,IdAccessLogin")] UserLogin userLogin)
        {
            if (id != userLogin.IdUser)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userLogin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserLoginExists(userLogin.IdUser))
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
            ViewData["IdAccessLogin"] = new SelectList(_context.AccessLogin, "IdAccessLogin", "IdAccessLogin", userLogin.IdAccessLogin);
            ViewData["IdUser"] = new SelectList(_context.User, "IdUser", "IdUser", userLogin.IdUser);
            return View(userLogin);
        }

        // GET: UserLogins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserLogin == null)
            {
                return NotFound();
            }

            var userLogin = await _context.UserLogin
                .Include(u => u.AccessLogin)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.IdUser == id);
            if (userLogin == null)
            {
                return NotFound();
            }

            return View(userLogin);
        }

        // POST: UserLogins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserLogin == null)
            {
                return Problem("Entity set 'ShareLoginDatabaseContext.UserLogin'  is null.");
            }
            var userLogin = await _context.UserLogin.FindAsync(id);
            if (userLogin != null)
            {
                _context.UserLogin.Remove(userLogin);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserLoginExists(int id)
        {
          return (_context.UserLogin?.Any(e => e.IdUser == id)).GetValueOrDefault();
        }
    }
}
