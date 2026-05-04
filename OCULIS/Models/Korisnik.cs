using System.ComponentModel.DataAnnotations;

namespace OCULIS.Models
{
    public class Korisnik
    {
        public Korisnik() { }

        [Key]
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public string Telefon { get; set; }
    }
}