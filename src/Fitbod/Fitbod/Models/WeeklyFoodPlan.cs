using System.ComponentModel.DataAnnotations;

namespace Fitbod.Models;

public class WeeklyFoodPlan
{
    [Key] public int WfpId { get; set; }
    [Required] public int Week { get; set; }
    [Required] public int Year { get; set; }
    
    public ICollection<WeekDay> WeekDay { get; set; }
}