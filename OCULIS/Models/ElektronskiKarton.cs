using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCULIS.Models
{
    public class ElektronskiKarton
    {
        public ElektronskiKarton() { }

        [Key]
        public int Id { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public string Napomena { get; set; }

        [ForeignKey("Korisnik")]
        public int IdKorisnik { get; set; }
        public Korisnik Korisnik { get; set; }
    }
}