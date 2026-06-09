using System.ComponentModel.DataAnnotations;

namespace OCULIS.Models.ViewModels
{
    public class NarudzbaCheckoutViewModel
    {
        public int KorpaId { get; set; }

        [Required(ErrorMessage = "Adresa isporuke je obavezna.")]
        [StringLength(250)]
        [Display(Name = "Adresa isporuke")]
        public string AdresaIsporuke { get; set; } = string.Empty;

        public PopustRezultatViewModel Popust { get; set; } = new();
        public KorpaViewModel Korpa { get; set; } = new();
    }
}
