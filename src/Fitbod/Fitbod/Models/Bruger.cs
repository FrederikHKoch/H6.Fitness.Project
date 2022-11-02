using System.ComponentModel.DataAnnotations;

namespace Fitbod.Models
{
    public class Bruger
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Fornavn { get; set; }
        [Required]
        public string Efternavn { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Køn { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
