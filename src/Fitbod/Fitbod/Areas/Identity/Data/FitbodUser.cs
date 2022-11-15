using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Fitbod.Areas.Identity.Data;

// Add profile data for application users by adding properties to the IdentityUser class
public class FitbodUser : IdentityUser
{
    [Required][RegularExpression(@"^[a-zA-Z\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed")] public string FirstName { get; set; }
    
    [Required][RegularExpression(@"^[a-zA-Z\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed")] public string LastName { get; set; }
}

