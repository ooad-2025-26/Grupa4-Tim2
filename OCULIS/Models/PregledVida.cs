using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCULIS.Models
{
    public class PregledVida
    {
        public PregledVida() { }

        [Key]
        public int Id { get; set; }
        public DateTime DatumPregleda { get; set; }
        public double DioptrijaLijevo { get; set; }
        public double DioptrijaDesno { get; set; }
        public string Preporuka { get; set; }

        [ForeignKey("ElektronskiKarton")]
        public int IdElektronskiKarton { get; set; }
        public ElektronskiKarton ElektronskiKarton { get; set; }
    }
}