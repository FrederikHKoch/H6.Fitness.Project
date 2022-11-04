using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitbod.Models;

public class RoleModel
{
    [Key] public int RoleId { get; set; }
    [Required] public string UserType { get; set; }
    //Foreign Key reference
    public UserModel UserModel { get; set; }
}