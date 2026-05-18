using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OCULIS.Models
{
    public class Korisnik : IdentityUser
    {
        [Required]
        public string Ime { get; set; }

        [Required]
        public string Prezime { get; set; }

        public string? Telefon { get; set; }
    }
}