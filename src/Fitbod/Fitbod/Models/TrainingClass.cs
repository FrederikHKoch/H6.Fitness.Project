using System.ComponentModel.DataAnnotations;

namespace Fitbod.Models
{
    public class TrainingClass
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int WeekNr { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Room { get; set; }
        [Required]
        public string Trainer { get; set; }
        [Required]
        public int MaxSignUp { get; set; }


    }
}
