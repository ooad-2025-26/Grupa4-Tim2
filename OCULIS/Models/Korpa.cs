using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCULIS.Models
{
    public class Korpa
    {
        public Korpa() { }

        [Key]
        public int Id { get; set; }
        public double UkupnaCijena { get; set; }

        [ForeignKey("Korisnik")]
        public int IdKorisnik { get; set; }
        public Korisnik Korisnik { get; set; }
    }
}