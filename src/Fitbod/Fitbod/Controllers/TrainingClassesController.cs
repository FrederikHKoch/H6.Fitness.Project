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
using Fitbod.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace Fitbod.Controllers
{
    [Authorize]
    public class TrainingClassesController : Controller
    {
        private readonly FitbodContext _context;
        private readonly UserManager<FitbodUser> _userManager;

        public TrainingClassesController(FitbodContext context, UserManager<FitbodUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        #region TrainingClass

        // GET: TrainingClasses
        public async Task<IActionResult> Index()
        {
            return View(await _context.TrainingClass.ToListAsync());
        }

        // GET: TrainingClasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrainingClasses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DateTime,Description,Room,Trainer,MaxSignUp,Signups")] TrainingClass trainingClass)
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
        [Authorize(Policy ="adminrights")]
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
        [HttpPost]
        [Authorize(Policy = "adminrights")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DateTime,Description,Room,Trainer,MaxSignUp,Signups")] TrainingClass trainingClass)
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
        [Authorize(Policy = "adminrights")]
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
        [Authorize(Policy = "adminrights")]
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
        #endregion  

        #region TeamSignUp

        // GET: TeamSignups/Index
        public async Task<IActionResult> SignupIndex()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var trainingclassentries = await _context.TeamSignUp.Include(x => x.TrainingClass).Where(x => x.FitbodUser.Id == user.Id).ToListAsync();

            return View("../TeamSignUps/Index", trainingclassentries);
        }

        //GET: TeamSignUps/Create/5
        public async Task<IActionResult> SignupCreate(int? id)
        {
            var trainingclass = await _context.TrainingClass.FirstOrDefaultAsync(x => x.Id == id);
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (trainingclass != null)
            {
                var teamsignupentry = await _context.TeamSignUp.FirstOrDefaultAsync(x => x.TrainingClassId == trainingclass.Id);

                if (teamsignupentry != null)
                {
                    return View("../TeamSignUps/Create");
                }
            }
            return NotFound();
        }

        // POST: TeamSignUps/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignupCreate([Bind("TeamSignUpId,TrainingClassId")] TeamSignUp teamSignUp, int? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            teamSignUp.FitbodUser = user;
            var trainingclass = _context.TrainingClass.FirstOrDefault(x => x.Id == id);
            var alltrainingclass = _context.TrainingClass;
            var teamsignupentry = _context.TeamSignUp.FirstOrDefault(x => x.FitbodUser.Id == user.Id && x.TrainingClassId == trainingclass.Id);

            if (teamsignupentry == null && trainingclass.Signups < trainingclass.MaxSignUp)
            {
                teamSignUp.TrainingClassId = trainingclass.Id;

                if (ModelState.IsValid)
                {
                    trainingclass.Signups++;
                    _context.Add(teamSignUp);
                    _context.Update(trainingclass);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            else if (teamsignupentry != null)
            {
                TempData["AllReadySignedError"] = "Du er allerede tilmeldt";
                ViewData["AllReadySignedError"] = TempData["AllReadySignedError"];
                return View(nameof(Index), alltrainingclass.ToList());
            }
            else if (trainingclass.Signups >= trainingclass.MaxSignUp)
            {
                TempData["FullTeamerror"] = "Holdet er fyldt";
                ViewData["FullTeamerror"] = TempData["FullTeamerror"];
                return View(nameof(Index), alltrainingclass.ToList());
            }
            return View(teamSignUp);
        }
        // GET: TeamSignUps/Delete/5
        public async Task<IActionResult> SignupDelete(int? id)
        {
            if (id == null || _context.TeamSignUp == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var teamSignUp = await _context.TeamSignUp.Include(x => x.FitbodUser).Where(x => x.FitbodUser.Id == user.Id).FirstOrDefaultAsync(x => x.TrainingClassId == id);
            var trainingclass = await _context.TrainingClass.FirstOrDefaultAsync(x => x.Id == id);

            if (trainingclass == null)
            {
                return NotFound();
            }

            if (teamSignUp == null)
            {
                var trainingClasses = _context.TrainingClass;
                TempData["AllReadySignedError"] = "Du er ikke tilmeldt";
                ViewData["AllReadySignedError"] = TempData["AllReadySignedError"];
                return View(nameof(Index), trainingClasses.ToList());
            }

            if (user.Id != teamSignUp.FitbodUser.Id)
            {
                return NotFound();
            }

            return View("../TeamSignUps/Delete", teamSignUp);
        }

        // POST: TeamSignUps/Delete/5
        [HttpPost, ActionName("SignupDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignupDeleteConfirmed(int TeamSignUpId)
        {
            if (_context.TeamSignUp == null)
            {
                return Problem("Entity set 'FitbodContext.TeamSignUp'  is null.");
            }

            var teamSignUp = await _context.TeamSignUp
                .Include(x => x.FitbodUser)
                .FirstOrDefaultAsync(x => x.TeamSignUpId == TeamSignUpId);

            var trainingclassentry = await _context.TrainingClass
                .FirstOrDefaultAsync(x => x.Id == teamSignUp.TrainingClassId);

            if (teamSignUp != null)
            {
                trainingclassentry.Signups--;
                _context.TrainingClass.Update(trainingclassentry);
                _context.TeamSignUp.Remove(teamSignUp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TeamSignUpExists(int id)
        {
            return _context.TeamSignUp.Any(e => e.TeamSignUpId == id);
        }
        #endregion
    }
}
