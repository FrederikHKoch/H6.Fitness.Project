using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitbod.Models
{
    public class Exercise
    {
        [Key]
        public int ExerciseId { get; set; }

        [Required(ErrorMessage = "Indtast navn")]
        [StringLength(50, ErrorMessage = "Navnet er for langt. Maks 50 tegn.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Indtast Muskelgruppe")]
        [StringLength(50, ErrorMessage = "Navnet er for langt. Maks 50 tegn.")]
        public string Musclegroup { get; set; }

        [Required(ErrorMessage = "Indtast Beskrivelse")]
        public string Description { get; set; }

        [Required]
        public string Image { get; set; }
        public ICollection<ExercisePlanEntry> ExercisePlanEntry { get; set; }

    }
}
