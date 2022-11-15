using System.Security.Claims;
using Fitbod.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Fitbod.Data;

public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<FitbodUser, IdentityRole>
{
    public ApplicationUserClaimsPrincipalFactory(
        UserManager<FitbodUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> options
    ) : base(userManager, roleManager, options)
    {
    }
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(FitbodUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);
        identity.AddClaim(new Claim("FirstName", user.FirstName));
        identity.AddClaim(new Claim("LastName", user.LastName));
        return identity;
    }
}