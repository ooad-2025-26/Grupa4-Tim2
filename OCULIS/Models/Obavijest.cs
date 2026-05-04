using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCULIS.Models
{
    public class Obavijest
    {
        public Obavijest() { }

        [Key]
        public int Id { get; set; }
        public string Naslov { get; set; }
        public string Tekst { get; set; }
        public DateTime DatumSlanja { get; set; }

        [ForeignKey("Korisnik")]
        public int IdKorisnik { get; set; }
        public Korisnik Korisnik { get; set; }
    }
}