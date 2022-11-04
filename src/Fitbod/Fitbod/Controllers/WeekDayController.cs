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
            var fitbodContext = _context.WeekDayModel.Include(w => w.DishModel).Include(w => w.WeeklyFoodPlanModel);
            return View(await fitbodContext.ToListAsync());
        }

        // GET: WeekDay/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WeekDayModel == null)
            {
                return NotFound();
            }

            var weekDayModel = await _context.WeekDayModel
                .Include(w => w.DishModel)
                .Include(w => w.WeeklyFoodPlanModel)
                .FirstOrDefaultAsync(m => m.WeekDayId == id);
            if (weekDayModel == null)
            {
                return NotFound();
            }

            return View(weekDayModel);
        }

        // GET: WeekDay/Create
        public IActionResult Create()
        {
            ViewData["DishId"] = new SelectList(_context.DishModel, "DishId", "Name");
            ViewData["WfpId"] = new SelectList(_context.Set<WeeklyFoodPlanModel>(), "WfpId", "WfpId");
            return View();
        }

        // POST: WeekDay/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WeekDayId,Day,DishId,WfpId")] WeekDayModel weekDayModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(weekDayModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DishId"] = new SelectList(_context.DishModel, "DishId", "Name", weekDayModel.DishId);
            ViewData["WfpId"] = new SelectList(_context.Set<WeeklyFoodPlanModel>(), "WfpId", "WfpId", weekDayModel.WfpId);
            return View(weekDayModel);
        }

        // GET: WeekDay/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WeekDayModel == null)
            {
                return NotFound();
            }

            var weekDayModel = await _context.WeekDayModel.FindAsync(id);
            if (weekDayModel == null)
            {
                return NotFound();
            }
            ViewData["DishId"] = new SelectList(_context.DishModel, "DishId", "Name", weekDayModel.DishId);
            ViewData["WfpId"] = new SelectList(_context.Set<WeeklyFoodPlanModel>(), "WfpId", "WfpId", weekDayModel.WfpId);
            return View(weekDayModel);
        }

        // POST: WeekDay/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WeekDayId,Day,DishId,WfpId")] WeekDayModel weekDayModel)
        {
            if (id != weekDayModel.WeekDayId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weekDayModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeekDayModelExists(weekDayModel.WeekDayId))
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
            ViewData["DishId"] = new SelectList(_context.DishModel, "DishId", "Name", weekDayModel.DishId);
            ViewData["WfpId"] = new SelectList(_context.Set<WeeklyFoodPlanModel>(), "WfpId", "WfpId", weekDayModel.WfpId);
            return View(weekDayModel);
        }

        // GET: WeekDay/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WeekDayModel == null)
            {
                return NotFound();
            }

            var weekDayModel = await _context.WeekDayModel
                .Include(w => w.DishModel)
                .Include(w => w.WeeklyFoodPlanModel)
                .FirstOrDefaultAsync(m => m.WeekDayId == id);
            if (weekDayModel == null)
            {
                return NotFound();
            }

            return View(weekDayModel);
        }

        // POST: WeekDay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WeekDayModel == null)
            {
                return Problem("Entity set 'FitbodContext.WeekDayModel'  is null.");
            }
            var weekDayModel = await _context.WeekDayModel.FindAsync(id);
            if (weekDayModel != null)
            {
                _context.WeekDayModel.Remove(weekDayModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeekDayModelExists(int id)
        {
          return _context.WeekDayModel.Any(e => e.WeekDayId == id);
        }
    }
}
