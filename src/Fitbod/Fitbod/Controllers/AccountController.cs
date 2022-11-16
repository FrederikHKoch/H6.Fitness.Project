using Fitbod.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fitbod.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly FitbodContext _context;

    public AccountController(FitbodContext context)
    {
        _context = context;
    }

    [AllowAnonymous]
    public ActionResult Login()
    {
        return null;
    }

    public ActionResult Logout()
    {
        return null;
    }
    // GETdotnet
}