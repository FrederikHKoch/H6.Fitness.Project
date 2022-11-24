using System.ComponentModel.DataAnnotations;

namespace Fitbod.Models;

public class Dish
{
    [Key] 
    public int DishId { get; set; }

    [Required(ErrorMessage = "Indtast navn")]
    [StringLength(50, ErrorMessage = "Navnet er for langt. Maks 50 tegn.")] 
    public string Name { get; set; }

    [Required(ErrorMessage = "Indtast URL")]
    [StringLength(50, ErrorMessage = "Navnet er for langt. Maks 50 tegn.")] 
    public string Url { get; set; }
    public WeekDay WeekDay { get; set; }
}