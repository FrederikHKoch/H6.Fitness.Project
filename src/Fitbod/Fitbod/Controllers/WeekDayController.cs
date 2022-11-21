using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fitbod.Data;
using Fitbod.Models;
using Microsoft.AspNetCore.Authorization;

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
            var fitbodContext = _context.WeekDay.Include(w => w.Dish).OrderBy(o=>o.Day).ToList();
            foreach (var item in fitbodContext)
            {
                int dayEnum = 0;
                
                if (int.TryParse(item.Day, out dayEnum))
                {
                    switch (dayEnum)
                    {
                        case 0:
                            item.Day = "Mandag";
                            break;
                        case 1:
                            item.Day = "Tirsdag";
                            break;
                        case 2:
                            item.Day = "Onsdag";
                            break;
                        case 3:
                            item.Day = "Torsdag";
                            break;
                        case 4:
                            item.Day = "Fredag";
                            break;
                        case 5:
                            item.Day = "Lørdag";
                            break;
                        case 6:
                            item.Day = "Søndag";
                            break;

                        default:
                            break;
                    }
                
                }
            
            }
            
            return View(fitbodContext);
        }


        // GET: WeekDay/Create
        public IActionResult Create()
        {
            ViewData["DishId"] = new SelectList(_context.Dish, "DishId", "Name");
            return View();
        }

        // POST: WeekDay/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WeekDayId,Day,DishId,WfpId")] WeekDay WeekDay)
        {
            var fitbodContext = _context.WeekDay.Include(w => w.Dish).OrderBy(o=>o.Day).ToList();
            bool ifExists = false;
            string errorMessage = "";
            
            if (ModelState.IsValid)
            {
                foreach (var item in fitbodContext)
                {
                    if (item.Day == WeekDay.Day && item.DishId == WeekDay.DishId)
                    {
                        ifExists = true;
                        errorMessage = "Retten og Dagen er allerede taget";
                        break;
                    }
                    if (item.Day == WeekDay.Day)
                    {
                        ifExists = true;
                        errorMessage = "Dagen er allerede taget";
                        break;
                    }
                    if (item.DishId == WeekDay.DishId)
                    {
                        ifExists = true;
                        errorMessage = "Retten er allerede taget";
                        break;
                    }
                }

                if (!ifExists)
                {
                    _context.Add(WeekDay);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["Error"] = errorMessage;
            ViewData["DishId"] = new SelectList(_context.Dish, "DishId", "Name", WeekDay.DishId);
            return View(WeekDay);
        }

        public async Task Test(WeekDay weekDay)
        {
            var fitbodContext = _context.WeekDay.Include(w => w.Dish).OrderBy(o=>o.Day).ToList();
            bool test = false;
            foreach (var item in fitbodContext)
            {
                if (item.Day == weekDay.Day)
                {
                    test = true;
                }
            }

            if (!test)
            {
                Create(weekDay);
            }
            else
            {
                
            }
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
