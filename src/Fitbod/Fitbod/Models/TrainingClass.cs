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
        [Range(1, 52)]
        public int WeekNr { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Room { get; set; }
        [Required]
        public string Trainer { get; set; }
        [Required]
        public int MaxSignUp { get; set; }
        public int Signups { get; set; }


    }
}
