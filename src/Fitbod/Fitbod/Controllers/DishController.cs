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
              return View(await _context.DishModel.ToListAsync());
        }

        // GET: Dish/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DishModel == null)
            {
                return NotFound();
            }

            var dishModel = await _context.DishModel
                .FirstOrDefaultAsync(m => m.DishId == id);
            if (dishModel == null)
            {
                return NotFound();
            }

            return View(dishModel);
        }

        // GET: Dish/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dish/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DishId,Name,Url")] DishModel dishModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dishModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dishModel);
        }

        // GET: Dish/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DishModel == null)
            {
                return NotFound();
            }

            var dishModel = await _context.DishModel.FindAsync(id);
            if (dishModel == null)
            {
                return NotFound();
            }
            return View(dishModel);
        }

        // POST: Dish/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DishId,Name,Url")] DishModel dishModel)
        {
            if (id != dishModel.DishId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dishModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishModelExists(dishModel.DishId))
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
            return View(dishModel);
        }

        // GET: Dish/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DishModel == null)
            {
                return NotFound();
            }

            var dishModel = await _context.DishModel
                .FirstOrDefaultAsync(m => m.DishId == id);
            if (dishModel == null)
            {
                return NotFound();
            }

            return View(dishModel);
        }

        // POST: Dish/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DishModel == null)
            {
                return Problem("Entity set 'FitbodContext.DishModel'  is null.");
            }
            var dishModel = await _context.DishModel.FindAsync(id);
            if (dishModel != null)
            {
                _context.DishModel.Remove(dishModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishModelExists(int id)
        {
          return _context.DishModel.Any(e => e.DishId == id);
        }
    }
}
