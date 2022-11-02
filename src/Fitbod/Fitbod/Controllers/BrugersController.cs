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
    public class BrugersController : Controller
    {
        private readonly FitbodContext _context;

        public BrugersController(FitbodContext context)
        {
            _context = context;
        }

        // GET: Brugers
        public async Task<IActionResult> Index()
        {
              return View(await _context.Bruger.ToListAsync());
        }

        // GET: Brugers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bruger == null)
            {
                return NotFound();
            }

            var bruger = await _context.Bruger
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bruger == null)
            {
                return NotFound();
            }

            return View(bruger);
        }

        // GET: Brugers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Brugers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fornavn,Efternavn,Email,Køn,Password")] Bruger bruger)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bruger);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bruger);
        }

        // GET: Brugers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bruger == null)
            {
                return NotFound();
            }

            var bruger = await _context.Bruger.FindAsync(id);
            if (bruger == null)
            {
                return NotFound();
            }
            return View(bruger);
        }

        // POST: Brugers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fornavn,Efternavn,Email,Køn,Password")] Bruger bruger)
        {
            if (id != bruger.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bruger);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrugerExists(bruger.Id))
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
            return View(bruger);
        }

        // GET: Brugers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bruger == null)
            {
                return NotFound();
            }

            var bruger = await _context.Bruger
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bruger == null)
            {
                return NotFound();
            }

            return View(bruger);
        }

        // POST: Brugers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bruger == null)
            {
                return Problem("Entity set 'FitbodContext.Bruger'  is null.");
            }
            var bruger = await _context.Bruger.FindAsync(id);
            if (bruger != null)
            {
                _context.Bruger.Remove(bruger);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrugerExists(int id)
        {
          return _context.Bruger.Any(e => e.Id == id);
        }
    }
}
