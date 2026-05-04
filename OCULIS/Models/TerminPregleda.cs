using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCULIS.Models
{
    public class TerminPregleda
    {
        public TerminPregleda() { }

        [Key]
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public TimeSpan Vrijeme { get; set; }
        public StatusTermina Status { get; set; }

        [ForeignKey("Poslovnica")]
        public int IdPoslovnica { get; set; }
        public Poslovnica Poslovnica { get; set; }

        [ForeignKey("Korisnik")]
        public int IdKorisnik { get; set; }
        public Korisnik Korisnik { get; set; }
    }
}