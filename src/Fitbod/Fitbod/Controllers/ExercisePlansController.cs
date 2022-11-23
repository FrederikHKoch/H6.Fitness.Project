using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fitbod.Data;
using Fitbod.Models;
using Fitbod.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Fitbod.Controllers
{
    [Authorize]
    public class ExercisePlansController : Controller
    {
        private readonly FitbodContext _context;
        private readonly UserManager<FitbodUser> _userManager;

        public ExercisePlansController(FitbodContext context, UserManager<FitbodUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: ExercisePlans
        public async Task<IActionResult> Index()
        {
            // TODO: Lav det om til logget-ind bruger
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var exercisePlanId = _context.ExercisePlan.Where(x => x.FitbodUser.Id == user.Id);
            return View(await exercisePlanId.ToListAsync());
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

        // GET: ExercisePlanEntryDetails/5
        public IActionResult ExercisePlanEntryDetails(int? id)
        {
            var exerciseplan = _context.ExercisePlan.FirstOrDefault(x => x.ExercisePlanId == id);

            if (id == null || _context.ExercisePlanEntry == null)
            {
                return NotFound();
            }

            var exercisePlanEntry = _context.ExercisePlanEntry.Include(u => u.Exercise).Where(x => x.ExercisePlanId == exerciseplan.ExercisePlanId).ToList();
            
            //Check if day is a number in database
            foreach (var item in exercisePlanEntry)
            {
                if (int.TryParse(item.Day, out var dayEnum))
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
                    }

                }

            }

            return View("../ExercisePlanEntries/Index", exercisePlanEntry);
        }

        // GET: ExercisePlans/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: ExercisePlans/AddExercise
        public IActionResult AddExercise(int? id)
        {

            ViewData["ExerciseId"] = new SelectList(_context.Set<Exercise>(), "ExerciseId", "Name");
            var exerciseplanid = _context.ExercisePlan.FirstOrDefault(x => x.ExercisePlanId == id);
            ViewData["ExercisePlanId"] = new SelectList(_context.Set<ExercisePlan>().Where(x => x.ExercisePlanId == id), "ExercisePlanId", "Name", exerciseplanid!.ExercisePlanId);
            return View("../ExercisePlanEntries/Create");
        }

        // POST: ExercisePlanEntries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePlanEntry([Bind("EntryId,Repetitions,Sets,Day,ExerciseId,ExercisePlanId")] ExercisePlanEntry exercisePlanEntry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exercisePlanEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExerciseId"] = new SelectList(_context.Set<Exercise>(), "ExerciseId", "Name", exercisePlanEntry.ExerciseId);
            return View(exercisePlanEntry);
        }

        // POST: ExercisePlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] ExercisePlan exercisePlan)
        {
            // TODO: Lav det om til logget-ind bruger
            var user = await _userManager.GetUserAsync(HttpContext.User);
            exercisePlan.FitbodUser = user;

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

        // GET: ExercisePlanEntries/Edit/5
        public async Task<IActionResult> EntryEdit(int? id)
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
            return View("../ExercisePlanEntries/Edit", exercisePlanEntry);
        }

        // POST: ExercisePlanEntries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EntryEdit(int id, [Bind("EntryId,Repetitions,Sets,Day,ExerciseId,ExercisePlanId")] ExercisePlanEntry exercisePlanEntry)
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
            return View("../ExercisePlanEntries/Edit", exercisePlanEntry);
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

            return View("../ExercisePlanEntries/Delete", exercisePlanEntry);
        }

        // POST: ExercisePlanEntries/Delete/5
        [HttpPost, ActionName("EntryDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EntryDeleteConfirmed(int id)
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

        private bool ExercisePlanExists(int id)
        {
            return _context.ExercisePlan.Any(e => e.ExercisePlanId == id);
        }
        private bool ExercisePlanEntryExists(int id)
        {
            return _context.ExercisePlanEntry.Any(e => e.EntryId == id);
        }
    }
}
