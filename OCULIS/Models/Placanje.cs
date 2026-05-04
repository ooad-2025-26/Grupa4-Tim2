using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCULIS.Models
{
    public class Placanje
    {
        public Placanje() { }

        [Key]
        public int Id { get; set; }
        public double Iznos { get; set; }
        public StatusPlacanja StatusPlacanja { get; set; }
        public DateTime DatumPlacanja { get; set; }

        [ForeignKey("Narudzba")]
        public int IdNarudzba { get; set; }
        public Narudzba Narudzba { get; set; }
    }
}