using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fitbod.Data;
using Fitbod.Models;

namespace Fitbod.Controllers
{
    public class TeamSignUpsController : Controller
    {
        private readonly FitbodContext _context;

        public TeamSignUpsController(FitbodContext context)
        {
            _context = context;
        }

        // GET: TeamSignUps
        public async Task<IActionResult> Index()
        {
              return View(await _context.TeamSignUp.ToListAsync());
        }

        // GET: TeamSignUps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TeamSignUp == null)
            {
                return NotFound();
            }

            var teamSignUp = await _context.TeamSignUp
                .FirstOrDefaultAsync(m => m.TeamSignUpId == id);
            if (teamSignUp == null)
            {
                return NotFound();
            }

            return View(teamSignUp);
        }

        // GET: TeamSignUps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TeamSignUps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamSignUpId")] TeamSignUp teamSignUp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teamSignUp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teamSignUp);
        }

        // GET: TeamSignUps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TeamSignUp == null)
            {
                return NotFound();
            }

            var teamSignUp = await _context.TeamSignUp.FindAsync(id);
            if (teamSignUp == null)
            {
                return NotFound();
            }
            return View(teamSignUp);
        }

        // POST: TeamSignUps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamSignUpId")] TeamSignUp teamSignUp)
        {
            if (id != teamSignUp.TeamSignUpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamSignUp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamSignUpExists(teamSignUp.TeamSignUpId))
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
            return View(teamSignUp);
        }

        // GET: TeamSignUps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TeamSignUp == null)
            {
                return NotFound();
            }

            var teamSignUp = await _context.TeamSignUp
                .FirstOrDefaultAsync(m => m.TeamSignUpId == id);
            if (teamSignUp == null)
            {
                return NotFound();
            }

            return View(teamSignUp);
        }

        // POST: TeamSignUps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TeamSignUp == null)
            {
                return Problem("Entity set 'FitbodContext.TeamSignUp'  is null.");
            }
            var teamSignUp = await _context.TeamSignUp.FindAsync(id);
            if (teamSignUp != null)
            {
                _context.TeamSignUp.Remove(teamSignUp);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamSignUpExists(int id)
        {
          return _context.TeamSignUp.Any(e => e.TeamSignUpId == id);
        }
    }
}
