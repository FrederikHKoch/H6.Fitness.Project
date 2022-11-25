using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fitbod.Areas.Identity.Data;

namespace Fitbod.Models
{
    public class ExercisePlan
    {
        [Key]
        public int ExercisePlanId { get; set; }

        [Required(ErrorMessage = "Indtast navn")]
        [StringLength(50, ErrorMessage = "Navnet er for langt. Maks 50 tegn.")]
        public string Name { get; set; }

        [ForeignKey("Id")]
        public FitbodUser FitbodUser { get; set; }

        public ICollection<ExercisePlanEntry> ExercisePlanEntries { get; set; }

    }
}
