using System.ComponentModel.DataAnnotations;
using OCULIS.Models;

namespace OCULIS.Models.ViewModels
{
    public class PlacanjeViewModel
    {
        public int IdNarudzba { get; set; }
        public double Iznos { get; set; }
        public string StatusNarudzbe { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Način plaćanja")]
        public NacinPlacanja NacinPlacanja { get; set; }

        [Display(Name = "Broj kartice")]
        [StringLength(19, MinimumLength = 13)]
        [RegularExpression(@"^[\d\s]+$", ErrorMessage = "Unesite ispravan broj kartice.")]
        public string? BrojKartice { get; set; }
    }
}
