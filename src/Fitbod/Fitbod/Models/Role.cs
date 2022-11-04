using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitbod.Models;

public class Role
{
    [Key] public int RoleId { get; set; }
    [Required] public string UserType { get; set; }
    //Foreign Key reference
    public User User { get; set; }
}