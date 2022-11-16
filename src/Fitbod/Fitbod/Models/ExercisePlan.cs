using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fitbod.Areas.Identity.Data;

namespace Fitbod.Models
{
    public class ExercisePlan
    {
        [Key]
        public int ExercisePlanId { get; set; }
        [Required]
        public string Name { get; set; }
        public int UserId { get; set; }

        [ForeignKey("Id")]
        public FitbodUser FitbodUser { get; set; }

        public ICollection<ExercisePlanEntry> ExercisePlanEntries { get; set; }

    }
}
