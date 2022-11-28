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

        #region ExercisePlan
        // GET: ExercisePlans
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var exercisePlanId = _context.ExercisePlan.Where(x => x.FitbodUser.Id == user.Id);
            return View(await exercisePlanId.ToListAsync());
        }

        // GET: ExercisePlans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExercisePlans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] ExercisePlan exercisePlan)
        {
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

        // GET: ExercisePlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var exercisePlan = await _context.ExercisePlan.FirstOrDefaultAsync(m => m.ExercisePlanId == id);

            if (id == null || _context.ExercisePlan == null)
            {
                return NotFound();
            }
            if (exercisePlan.FitbodUser != null && exercisePlan.FitbodUser.Id == user.Id)
            {
                if (exercisePlan == null)
                {
                    return NotFound();
                }

                return View(exercisePlan);
            }
            return NotFound();
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
        #endregion

        #region Entry
        // GET: EntryIndex/5
        public async Task<IActionResult> EntryIndex(int? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var exercisePlan = await _context.ExercisePlan.FirstOrDefaultAsync(x => x.ExercisePlanId == id);
            if (exercisePlan != null)
            {

                if (id == null || _context.ExercisePlanEntry == null)
                {
                    return NotFound();
                }
                if (exercisePlan.FitbodUser != null && exercisePlan.FitbodUser.Id == user.Id)
                {
                    var exercisePlanEntry = _context.ExercisePlanEntry.Include(u => u.Exercise).Where(x => x.ExercisePlanId == exercisePlan.ExercisePlanId).ToList();

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
            }
            return NotFound();
        }
        
        // GET: ExercisePlans/EntryCreate/5
        public async Task<IActionResult> EntryCreate(int? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var exercisePlan = await _context.ExercisePlan.FirstOrDefaultAsync(x => x.ExercisePlanId == id);

            if (exercisePlan.FitbodUser != null && exercisePlan.FitbodUser.Id == user.Id)
            {
                ViewData["ExerciseId"] = new SelectList(_context.Set<Exercise>(), "ExerciseId", "Name");
                ViewData["ExercisePlanId"] = new SelectList(_context.Set<ExercisePlan>().Where(x => x.ExercisePlanId == id), "ExercisePlanId", "Name", exercisePlan!.ExercisePlanId);
                return View("../ExercisePlanEntries/Create");
            }
            return NotFound();
        }
        
        // POST: ExercisePlans/EntryCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EntryCreate([Bind("EntryId,Repetitions,Sets,Day,ExerciseId,ExercisePlanId")] ExercisePlanEntry exercisePlanEntry)
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
       
        // GET: ExercisePlanEntries/Edit/5
        public async Task<IActionResult> EntryEdit(int? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var exercisePlanEntry = await _context.ExercisePlanEntry.FindAsync(id);
            if (exercisePlanEntry != null)
            {

                var exerciseplan = await _context.ExercisePlan.FindAsync(exercisePlanEntry.ExercisePlanId);

                if (id == null || _context.ExercisePlanEntry == null)
                {
                    return NotFound();
                }

                if (exerciseplan.FitbodUser != null && exerciseplan.FitbodUser.Id == user.Id)
                {
                    if (exercisePlanEntry == null)
                    {
                        return NotFound();
                    }
                    return View("../ExercisePlanEntries/Edit", exercisePlanEntry);
                }
            }
            return NotFound();
        }
        
        // POST: ExercisePlanEntries/Edit/5
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
        
        // GET: ExercisePlanEntries/Delete/5
        public async Task<IActionResult> EntryDelete(int? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var exercisePlanEntry = await _context.ExercisePlanEntry.FirstOrDefaultAsync(m => m.EntryId == id);
            var exerciseplan = await _context.ExercisePlan.FindAsync(exercisePlanEntry.ExercisePlanId);

            if (id == null || _context.ExercisePlanEntry == null)
            {
                return NotFound();
            }
            if (exerciseplan.FitbodUser != null && exerciseplan.FitbodUser.Id == user.Id)
            {
                if (exercisePlanEntry == null)
                {
                    return NotFound();
                }

                return View("../ExercisePlanEntries/Delete", exercisePlanEntry);
            }
            return NotFound();
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

        private bool ExercisePlanEntryExists(int id)
        {
            return _context.ExercisePlanEntry.Any(e => e.EntryId == id);
        }
        #endregion
    }
}
