using System.ComponentModel.DataAnnotations;

namespace Fitbod.Models;

public class Dish
{
    [Key] public int DishId { get; set; }
    [Required(ErrorMessage = "Indtast navn")] public string Name { get; set; }
    [Required(ErrorMessage = "Indtast URL")] public string Url { get; set; }
    public WeekDay WeekDay { get; set; }
}