using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fitbod.Controllers;

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
    
    [Authorize(Policy = "rolecreation")]
    public IActionResult Create()
    {
        return View(new IdentityRole());
    }
    
    [HttpPost]
    [Authorize(Policy = "rolecreation")]
    public async Task<IActionResult> Create(IdentityRole role)
    {
        await roleManager.CreateAsync(role);
        return RedirectToAction("Index");
    }
}