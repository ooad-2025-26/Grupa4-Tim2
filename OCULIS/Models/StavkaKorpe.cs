using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCULIS.Models
{
    public class StavkaKorpe
    {
        public StavkaKorpe() { }

        [Key]
        public int Id { get; set; }
        public int Kolicina { get; set; }
        public double Cijena { get; set; }

        [ForeignKey("Proizvod")]
        public int IdProizvod { get; set; }
        public Proizvod Proizvod { get; set; }

        [ForeignKey("Korpa")]
        public int IdKorpa { get; set; }
        public Korpa Korpa { get; set; }
    }
}