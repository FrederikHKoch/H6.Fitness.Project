using System.ComponentModel.DataAnnotations;

namespace Fitbod.Models
{
    public class TeamSignUp
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public TrainingClass TrainingClass { get; set; }

        public DateTime Date { get; set; }

    }
}
