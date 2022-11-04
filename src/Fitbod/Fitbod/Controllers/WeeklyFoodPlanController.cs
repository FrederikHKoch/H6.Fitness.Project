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
    public class WeeklyFoodPlanController : Controller
    {
        private readonly FitbodContext _context;

        public WeeklyFoodPlanController(FitbodContext context)
        {
            _context = context;
        }

        // GET: WeeklyFoodPlan
        public async Task<IActionResult> Index()
        {
              return View(await _context.WeeklyFoodPlan.ToListAsync());
        }

        // GET: WeeklyFoodPlan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WeeklyFoodPlan == null)
            {
                return NotFound();
            }

            var WeeklyFoodPlan = await _context.WeeklyFoodPlan
                .FirstOrDefaultAsync(m => m.WfpId == id);
            if (WeeklyFoodPlan == null)
            {
                return NotFound();
            }

            return View(WeeklyFoodPlan);
        }

        // GET: WeeklyFoodPlan/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WeeklyFoodPlan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WfpId,Week,Year")] WeeklyFoodPlan WeeklyFoodPlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(WeeklyFoodPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(WeeklyFoodPlan);
        }

        // GET: WeeklyFoodPlan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WeeklyFoodPlan == null)
            {
                return NotFound();
            }

            var WeeklyFoodPlan = await _context.WeeklyFoodPlan.FindAsync(id);
            if (WeeklyFoodPlan == null)
            {
                return NotFound();
            }
            return View(WeeklyFoodPlan);
        }

        // POST: WeeklyFoodPlan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WfpId,Week,Year")] WeeklyFoodPlan WeeklyFoodPlan)
        {
            if (id != WeeklyFoodPlan.WfpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(WeeklyFoodPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeeklyFoodPlanModelExists(WeeklyFoodPlan.WfpId))
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
            return View(WeeklyFoodPlan);
        }

        // GET: WeeklyFoodPlan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WeeklyFoodPlan == null)
            {
                return NotFound();
            }

            var WeeklyFoodPlan = await _context.WeeklyFoodPlan
                .FirstOrDefaultAsync(m => m.WfpId == id);
            if (WeeklyFoodPlan == null)
            {
                return NotFound();
            }

            return View(WeeklyFoodPlan);
        }

        // POST: WeeklyFoodPlan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WeeklyFoodPlan == null)
            {
                return Problem("Entity set 'FitbodContext.WeeklyFoodPlan'  is null.");
            }
            var WeeklyFoodPlan = await _context.WeeklyFoodPlan.FindAsync(id);
            if (WeeklyFoodPlan != null)
            {
                _context.WeeklyFoodPlan.Remove(WeeklyFoodPlan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeeklyFoodPlanModelExists(int id)
        {
          return _context.WeeklyFoodPlan.Any(e => e.WfpId == id);
        }
    }
}
