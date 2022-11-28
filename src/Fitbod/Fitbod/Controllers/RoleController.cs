using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fitbod.Controllers;

[Authorize(Policy = "superuserrights")]
public class RoleController : Controller
{
    RoleManager<IdentityRole> roleManager;
    
    public RoleController(RoleManager<IdentityRole> roleManager)
    {
        this.roleManager = roleManager;
    }
    
    public IActionResult Index()
    {
        var roles = roleManager.Roles.ToList();
        return View(roles);
    }
    
    public IActionResult Create()
    {
        return View(new IdentityRole());
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(IdentityRole role)
    {
        await roleManager.CreateAsync(role);
        return RedirectToAction("Index");
    }
}