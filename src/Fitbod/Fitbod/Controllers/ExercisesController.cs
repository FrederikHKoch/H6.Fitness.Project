using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fitbod.Data;
using Fitbod.Models;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.Metrics;
using Fitbod.ViewModel;

namespace Fitbod.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly FitbodContext _context;
        private readonly IWebHostEnvironment _env;

        public ExercisesController(FitbodContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Exercises
        public async Task<IActionResult> Index()
        {
              return View(await _context.Exercise.ToListAsync());
        }
        public async Task<IActionResult> IndexBiceps()
        {
            return View(await _context.Exercise.Where(x => x.Musclegroup == "Biceps").ToListAsync());
        }

        public async Task<IActionResult> IndexAbs()
        {
            return View(await _context.Exercise.Where(x => x.Musclegroup == "Mavemuskler").ToListAsync());
        }

        public async Task<IActionResult> IndexBack()
        {
            return View(await _context.Exercise.Where(x => x.Musclegroup == "Ryg").ToListAsync());
        }
        public async Task<IActionResult> IndexCalves()
        {
            return View(await _context.Exercise.Where(x => x.Musclegroup == "Læg").ToListAsync());
        }
        public async Task<IActionResult> IndexChest()
        {
            return View(await _context.Exercise.Where(x => x.Musclegroup == "Bryst").ToListAsync());
        }
        public async Task<IActionResult> IndexHamstring()
        {
            return View(await _context.Exercise.Where(x => x.Musclegroup == "Baglår").ToListAsync());
        }
        public async Task<IActionResult> IndexShoulders()
        {
            return View(await _context.Exercise.Where(x => x.Musclegroup == "Skulder").ToListAsync());
        }
        public async Task<IActionResult> IndexTriceps()
        {
            return View(await _context.Exercise.Where(x => x.Musclegroup == "Triceps").ToListAsync());
        }
        
        // GET: Exercises/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExercise(ExercisesViewModel model, IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return Content("File not selected");
            }
            var path = Path.Combine("../Fitbod/wwwroot/img/exercises", image.FileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(stream);
                stream.Close();
            }

            model.Exercises.Image = image.FileName;

            if (model != null)
            {
                var exercise = new Exercise
                {
                    Name = model.Exercises.Name,
                    Musclegroup = model.Exercises.Musclegroup,
                    Description = model.Exercises.Description,
                    Image = model.Exercises.Image,
                    //ImageLocation = path,
                };
                _context.Add(exercise);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");

        }

        // GET: Exercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Exercise == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercise.FindAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }
            return View(exercise);
        }

        //POST: Exercises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExerciseId,Name,Musclegroup,Description,Image")] Exercise exercise)
        {
            if (id != exercise.ExerciseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseExists(exercise.ExerciseId))
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
            return View(exercise);
        }

        // GET: Exercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Exercise == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercise
                .FirstOrDefaultAsync(m => m.ExerciseId == id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Exercise == null)
            {
                return Problem("Entity set 'FitbodContext.Exercise'  is null.");
            }
            var exercise = await _context.Exercise.FindAsync(id);
            if (exercise != null)
            {
                _context.Exercise.Remove(exercise);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseExists(int id)
        {
          return _context.Exercise.Any(e => e.ExerciseId == id);
        }
    }
}
