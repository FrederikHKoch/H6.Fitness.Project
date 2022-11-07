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
    public class TrainingClassesController : Controller
    {
        private readonly FitbodContext _context;

        public TrainingClassesController(FitbodContext context)
        {
            _context = context;
        }

        // GET: TrainingClasses
        public async Task<IActionResult> Index()
        {
              return View(await _context.TrainingClass.ToListAsync());
        }

        // GET: TrainingClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TrainingClass == null)
            {
                return NotFound();
            }

            var trainingClass = await _context.TrainingClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingClass == null)
            {
                return NotFound();
            }

            return View(trainingClass);
        }

        // GET: TrainingClasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrainingClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,WeekNr,Date,Description,Room,Trainer,MaxSignUp")] TrainingClass trainingClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainingClass);
        }

        // GET: TrainingClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TrainingClass == null)
            {
                return NotFound();
            }

            var trainingClass = await _context.TrainingClass.FindAsync(id);
            if (trainingClass == null)
            {
                return NotFound();
            }
            return View(trainingClass);
        }

        // POST: TrainingClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,WeekNr,Date,Description,Room,Trainer,MaxSignUp")] TrainingClass trainingClass)
        {
            if (id != trainingClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingClassExists(trainingClass.Id))
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
            return View(trainingClass);
        }

        // GET: TrainingClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TrainingClass == null)
            {
                return NotFound();
            }

            var trainingClass = await _context.TrainingClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingClass == null)
            {
                return NotFound();
            }

            return View(trainingClass);
        }

        // POST: TrainingClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TrainingClass == null)
            {
                return Problem("Entity set 'FitbodContext.TrainingClass'  is null.");
            }
            var trainingClass = await _context.TrainingClass.FindAsync(id);
            if (trainingClass != null)
            {
                _context.TrainingClass.Remove(trainingClass);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingClassExists(int id)
        {
          return _context.TrainingClass.Any(e => e.Id == id);
        }
    }
}
