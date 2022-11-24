using Fitbod.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitbod.Models
{
    public class ExercisePlanEntry
    {
        [Key]
        public int EntryId { get; set; }

        [Required(ErrorMessage = "Indtast antal reps")]
        [Range(1, 100, ErrorMessage ="Mellem 1 og 100")]
        public int Repetitions { get; set; }
        [Required(ErrorMessage = "Indtast antal sets")]
        [Range(1, 100, ErrorMessage ="Mellem 1 og 100")]
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
