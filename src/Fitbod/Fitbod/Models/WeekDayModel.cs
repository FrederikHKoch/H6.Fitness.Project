using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitbod.Models;

public class WeekDayModel
{
    [Key] public int WeekDayId { get; set; }
    [Required] public string Day { get; set; }
    
    public int DishId { get; set; }
    [ForeignKey("DishId")] public DishModel DishModel { get; set; }
    
    public int WfpId { get; set; }
    [ForeignKey("WfpId")]public WeeklyFoodPlanModel WeeklyFoodPlanModel { get; set; }
}