using System.ComponentModel.DataAnnotations;

namespace OCULIS.Models
{
    public class Poslovnica
    {
        public Poslovnica() { }

        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }
        public string RadnoVrijeme { get; set; }
    }
}