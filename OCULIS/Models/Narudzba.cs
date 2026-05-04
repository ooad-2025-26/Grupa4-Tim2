using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCULIS.Models
{
    public class Narudzba
    {
        public Narudzba() { }

        [Key]
        public int Id { get; set; }
        public DateTime DatumNarudzbe { get; set; }
        public StatusNarudzbe Status { get; set; }
        public double UkupnaCijena { get; set; }
        public string AdresaIsporuke { get; set; }

        [ForeignKey("Korisnik")]
        public int IdKorisnik { get; set; }
        public Korisnik Korisnik { get; set; }

        [ForeignKey("Korpa")]
        public int IdKorpa { get; set; }
        public Korpa Korpa { get; set; }
    }
}