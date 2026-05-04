using System.ComponentModel.DataAnnotations;

namespace OCULIS.Models
{
    public class Proizvod
    {
        public Proizvod() { }

        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public double Cijena { get; set; }
        public KategorijaProizvoda Kategorija { get; set; }
        public int DostupnaKolicina { get; set; }
    }
}