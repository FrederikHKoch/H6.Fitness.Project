using System.ComponentModel.DataAnnotations;

namespace Fitbod.Models;

public class WeeklyFoodPlanModel
{
    [Key] public int WfpId { get; set; }
    [Required] public int Week { get; set; }
    [Required] public int Year { get; set; }
    
    public ICollection<WeekDayModel> WeekDayModels { get; set; }
}