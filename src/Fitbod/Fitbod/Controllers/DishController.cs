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
    [Authorize(Policy = "adminrights")]
    public class DishController : Controller
    {
        private readonly FitbodContext _context;

        public DishController(FitbodContext context)
        {
            _context = context;
        }

        // GET: Dish
        public async Task<IActionResult> Index()
        {
              return View(await _context.Dish.ToListAsync());
        }

        // GET: Dish/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dish/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DishId,Name,Url")] Dish Dish)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Dish);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Dish);
        }

        // GET: Dish/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Dish == null)
            {
                return NotFound();
            }

            var Dish = await _context.Dish.FindAsync(id);
            if (Dish == null)
            {
                return NotFound();
            }
            return View(Dish);
        }

        // POST: Dish/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DishId,Name,Url")] Dish Dish)
        {
            if (id != Dish.DishId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Dish);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishModelExists(Dish.DishId))
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
            return View(Dish);
        }

        // GET: Dish/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Dish == null)
            {
                return NotFound();
            }

            var Dish = await _context.Dish
                .FirstOrDefaultAsync(m => m.DishId == id);
            if (Dish == null)
            {
                return NotFound();
            }

            return View(Dish);
        }

        // POST: Dish/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Dish == null)
            {
                return Problem("Entity set 'FitbodContext.Dish'  is null.");
            }
            var Dish = await _context.Dish.FindAsync(id);
            if (Dish != null)
            {
                _context.Dish.Remove(Dish);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishModelExists(int id)
        {
          return _context.Dish.Any(e => e.DishId == id);
        }
    }
}
