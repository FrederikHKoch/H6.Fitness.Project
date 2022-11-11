using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitbod.Models;

public class User
{
    [Key] public int UserId { get; set; }
    [Required][RegularExpression(@"^[a-zA-Z\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed")] public string FirstName { get; set; }
    [Required][RegularExpression(@"^[a-zA-Z\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed")] public string LastName { get; set; }
    [Required][EmailAddress] public string Email { get; set; }
    public string Gender { get; set; }
    [Required][PasswordPropertyText][StringLength(40, ErrorMessage = "Password must be between 6 and 40 characters", MinimumLength=6)] public string Password { get; set; }
    public int RoleId { get; set; }
    [ForeignKey("RoleId")] public Role Role { get; set; }

}