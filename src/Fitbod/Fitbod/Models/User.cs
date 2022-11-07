using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitbod.Models;

public class User
{
    [Key] public int UserId { get; set; }
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    [Required] public string Email { get; set; }
    public string Gender { get; set; }
    [Required] public string Password { get; set; }
    public int RoleId { get; set; }
    [ForeignKey("RoleId")] public Role Role { get; set; }

}