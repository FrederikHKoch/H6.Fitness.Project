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
    public class WeekDayController : Controller
    {
        private readonly FitbodContext _context;

        public WeekDayController(FitbodContext context)
        {
            _context = context;
        }

        // GET: WeekDay
        public async Task<IActionResult> Index()
        {
            var fitbodContext = _context.WeekDay.Include(w => w.Dish).Include(w => w.WeeklyFoodPlan);
            return View(await fitbodContext.ToListAsync());
        }

        // GET: WeekDay/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WeekDay == null)
            {
                return NotFound();
            }

            var WeekDay = await _context.WeekDay
                .Include(w => w.Dish)
                .Include(w => w.WeeklyFoodPlan)
                .FirstOrDefaultAsync(m => m.WeekDayId == id);
            if (WeekDay == null)
            {
                return NotFound();
            }

            return View(WeekDay);
        }

        // GET: WeekDay/Create
        public IActionResult Create()
        {
            ViewData["DishId"] = new SelectList(_context.Dish, "DishId", "Name");
            ViewData["WfpId"] = new SelectList(_context.Set<WeeklyFoodPlan>(), "WfpId", "WfpId");
            return View();
        }

        // POST: WeekDay/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WeekDayId,Day,DishId,WfpId")] WeekDay WeekDay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(WeekDay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DishId"] = new SelectList(_context.Dish, "DishId", "Name", WeekDay.DishId);
            ViewData["WfpId"] = new SelectList(_context.Set<WeeklyFoodPlan>(), "WfpId", "WfpId", WeekDay.WfpId);
            return View(WeekDay);
        }

        // GET: WeekDay/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WeekDay == null)
            {
                return NotFound();
            }

            var WeekDay = await _context.WeekDay.FindAsync(id);
            if (WeekDay == null)
            {
                return NotFound();
            }
            ViewData["DishId"] = new SelectList(_context.Dish, "DishId", "Name", WeekDay.DishId);
            ViewData["WfpId"] = new SelectList(_context.Set<WeeklyFoodPlan>(), "WfpId", "WfpId", WeekDay.WfpId);
            return View(WeekDay);
        }

        // POST: WeekDay/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WeekDayId,Day,DishId,WfpId")] WeekDay WeekDay)
        {
            if (id != WeekDay.WeekDayId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(WeekDay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeekDayModelExists(WeekDay.WeekDayId))
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
            ViewData["DishId"] = new SelectList(_context.Dish, "DishId", "Name", WeekDay.DishId);
            ViewData["WfpId"] = new SelectList(_context.Set<WeeklyFoodPlan>(), "WfpId", "WfpId", WeekDay.WfpId);
            return View(WeekDay);
        }

        // GET: WeekDay/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WeekDay == null)
            {
                return NotFound();
            }

            var WeekDay = await _context.WeekDay
                .Include(w => w.Dish)
                .Include(w => w.WeeklyFoodPlan)
                .FirstOrDefaultAsync(m => m.WeekDayId == id);
            if (WeekDay == null)
            {
                return NotFound();
            }

            return View(WeekDay);
        }

        // POST: WeekDay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WeekDay == null)
            {
                return Problem("Entity set 'FitbodContext.WeekDay'  is null.");
            }
            var WeekDay = await _context.WeekDay.FindAsync(id);
            if (WeekDay != null)
            {
                _context.WeekDay.Remove(WeekDay);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeekDayModelExists(int id)
        {
          return _context.WeekDay.Any(e => e.WeekDayId == id);
        }
    }
}
