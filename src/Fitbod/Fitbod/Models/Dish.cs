using System.ComponentModel.DataAnnotations;

namespace Fitbod.Models;

public class Dish
{
    [Key] 
    public int DishId { get; set; }

    [Required(ErrorMessage = "Indtast navn")]
    [StringLength(100, ErrorMessage = "Maks 100 tegn.")] 
    public string Name { get; set; }

    [Required(ErrorMessage = "Indtast URL")]
    public string Url { get; set; }
    public WeekDay WeekDay { get; set; }
}