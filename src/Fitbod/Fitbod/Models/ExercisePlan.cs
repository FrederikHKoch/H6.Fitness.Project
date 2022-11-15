using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitbod.Models
{
    public class ExercisePlan
    {
        [Key]
        public int ExercisePlanId { get; set; }
        [Required]
        public string Name { get; set; }

        // [Required]
        // public User User { get; set; }

    }
}
