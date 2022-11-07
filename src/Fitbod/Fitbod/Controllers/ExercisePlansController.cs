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
    public class ExercisePlansController : Controller
    {
        private readonly FitbodContext _context;

        public ExercisePlansController(FitbodContext context)
        {
            _context = context;
        }

        // GET: ExercisePlans
        public async Task<IActionResult> Index()
        {
              return View(await _context.ExercisePlan.ToListAsync());
        }

        // GET: ExercisePlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ExercisePlan == null)
            {
                return NotFound();
            }

            var exercisePlan = await _context.ExercisePlan
                .FirstOrDefaultAsync(m => m.ExercisePlanId == id);
            if (exercisePlan == null)
            {
                return NotFound();
            }

            return View(exercisePlan);
        }

        // GET: ExercisePlans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExercisePlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExercisePlanId,Name")] ExercisePlan exercisePlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exercisePlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exercisePlan);
        }

        // GET: ExercisePlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ExercisePlan == null)
            {
                return NotFound();
            }

            var exercisePlan = await _context.ExercisePlan.FindAsync(id);
            if (exercisePlan == null)
            {
                return NotFound();
            }
            return View(exercisePlan);
        }

        // POST: ExercisePlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExercisePlanId,Name")] ExercisePlan exercisePlan)
        {
            if (id != exercisePlan.ExercisePlanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exercisePlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExercisePlanExists(exercisePlan.ExercisePlanId))
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
            return View(exercisePlan);
        }

        // GET: ExercisePlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ExercisePlan == null)
            {
                return NotFound();
            }

            var exercisePlan = await _context.ExercisePlan
                .FirstOrDefaultAsync(m => m.ExercisePlanId == id);
            if (exercisePlan == null)
            {
                return NotFound();
            }

            return View(exercisePlan);
        }

        // POST: ExercisePlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ExercisePlan == null)
            {
                return Problem("Entity set 'FitbodContext.ExercisePlan'  is null.");
            }
            var exercisePlan = await _context.ExercisePlan.FindAsync(id);
            if (exercisePlan != null)
            {
                _context.ExercisePlan.Remove(exercisePlan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExercisePlanExists(int id)
        {
          return _context.ExercisePlan.Any(e => e.ExercisePlanId == id);
        }
    }
}
