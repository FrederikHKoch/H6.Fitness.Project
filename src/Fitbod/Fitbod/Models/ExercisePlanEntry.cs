using Fitbod.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitbod.Models
{
    public class ExercisePlanEntry
    {
        [Key]
        public int EntryId { get; set; }
        [Required]
        public int Repetitions { get; set; }
        [Required]
        public int Sets { get; set; }
        [Required]
        public string Day { get; set; }
        
        public int ExerciseId { get; set; }
        
        [ForeignKey("ExerciseId")]
        public Exercise Exercise { get; set; }
        
        public int ExercisePlanId { get; set; }

        [ForeignKey("ExercisePlanId")]
        public ExercisePlan ExercisePlan { get; set; }

    }
}
