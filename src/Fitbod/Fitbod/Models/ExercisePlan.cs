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
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<ExercisePlanEntry> ExercisePlanEntries { get; set; }
    }
}
