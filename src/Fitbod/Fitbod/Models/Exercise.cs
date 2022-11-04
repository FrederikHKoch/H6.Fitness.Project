using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitbod.Models
{
    public class Exercise
    {
        [Key]
        public int ExerciseId { get; set; }
        [Required] 
        public string Name { get; set; }
        [Required]
        public string Musclegroup { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }

        [Required]
        public ExercisePlanEntry Entry { get; set; }

    }
}
