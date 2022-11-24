using System.ComponentModel.DataAnnotations;

namespace Fitbod.Models
{
    public class TrainingClass
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Indtast navn")]
        [StringLength(50, ErrorMessage = "Navnet er for langt. Maks 50 tegn.")]
        public string Name { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateTime { get; set; }

        [Required(ErrorMessage ="Indtast beskrivelse")]
        [StringLength(500, ErrorMessage = "Maks 500 tegn.")]
        public string Description { get; set; }

        [Required(ErrorMessage ="Indtast rum")]
        [StringLength(50, ErrorMessage = "Maks 50 tegn.")]
        public string Room { get; set; }

        [Required(ErrorMessage ="Indtast navn på træner")]
        [StringLength(50, ErrorMessage = "Navnet er for langt. Maks 50 tegn.")]
        public string Trainer { get; set; }

        [Required]
        [Range(1, 50)]
        public int MaxSignUp { get; set; }

        public int Signups { get; set; }

        public ICollection<TeamSignUp> TeamSignUps { get; set; }
    }
}
