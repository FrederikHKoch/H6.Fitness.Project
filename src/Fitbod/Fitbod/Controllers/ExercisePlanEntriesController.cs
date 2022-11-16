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
    public class ExercisePlanEntriesController : Controller
    {
        private readonly FitbodContext _context;

        public ExercisePlanEntriesController(FitbodContext context)
        {
            _context = context;
        }

        // GET: ExercisePlanEntries
        public async Task<IActionResult> Index()
        {
              return View(await _context.ExercisePlanEntry.ToListAsync());
        }

        // GET: ExercisePlanEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ExercisePlanEntry == null)
            {
                return NotFound();
            }

            var exercisePlanEntry = await _context.ExercisePlanEntry
                .FirstOrDefaultAsync(m => m.EntryId == id);
            if (exercisePlanEntry == null)
            {
                return NotFound();
            }

            return View(exercisePlanEntry);
        }

        // GET: ExercisePlanEntries/Create
        public IActionResult Create()
        {            
            return View();
        }

        // POST: ExercisePlanEntries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EntryId,Repetitions,Sets,Day")] ExercisePlanEntry exercisePlanEntry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exercisePlanEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exercisePlanEntry);
        }

        // GET: ExercisePlanEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ExercisePlanEntry == null)
            {
                return NotFound();
            }

            var exercisePlanEntry = await _context.ExercisePlanEntry.FindAsync(id);
            if (exercisePlanEntry == null)
            {
                return NotFound();
            }
            return View(exercisePlanEntry);
        }

        // POST: ExercisePlanEntries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EntryId,Repetitions,Sets,Day")] ExercisePlanEntry exercisePlanEntry)
        {
            if (id != exercisePlanEntry.EntryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exercisePlanEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExercisePlanEntryExists(exercisePlanEntry.EntryId))
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
            return View(exercisePlanEntry);
        }

        // GET: ExercisePlanEntries/Delete/5
        public async Task<IActionResult> EntryDelete(int? id)
        {
            if (id == null || _context.ExercisePlanEntry == null)
            {
                return NotFound();
            }

            var exercisePlanEntry = await _context.ExercisePlanEntry
                .FirstOrDefaultAsync(m => m.EntryId == id);
            if (exercisePlanEntry == null)
            {
                return NotFound();
            }

            return View(exercisePlanEntry);
        }

        // POST: ExercisePlanEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ExercisePlanEntry == null)
            {
                return Problem("Entity set 'FitbodContext.ExercisePlanEntry'  is null.");
            }
            var exercisePlanEntry = await _context.ExercisePlanEntry.FindAsync(id);
            if (exercisePlanEntry != null)
            {
                _context.ExercisePlanEntry.Remove(exercisePlanEntry);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExercisePlanEntryExists(int id)
        {
          return _context.ExercisePlanEntry.Any(e => e.EntryId == id);
        }
    }
}
