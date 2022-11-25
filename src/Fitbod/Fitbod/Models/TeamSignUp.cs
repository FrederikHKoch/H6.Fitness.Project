using Fitbod.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitbod.Models
{
    public class TeamSignUp
    {
        [Key]
        public int TeamSignUpId { get; set; }

        [ForeignKey("Id")]
        public FitbodUser FitbodUser { get; set; }

        [ForeignKey("Id")] public int TrainingClassId { get; set; }
        public TrainingClass TrainingClass { get; set; }
    }
}
