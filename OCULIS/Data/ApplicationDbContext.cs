using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OCULIS.Models;

namespace OCULIS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<Proizvod> Proizvod { get; set; }
        public DbSet<Korpa> Korpa { get; set; }
        public DbSet<StavkaKorpe> StavkaKorpe { get; set; }
        public DbSet<Narudzba> Narudzba { get; set; }
        public DbSet<Placanje> Placanje { get; set; }
        public DbSet<Obavijest> Obavijest { get; set; }
        public DbSet<Poslovnica> Poslovnica { get; set; }
        public DbSet<TerminPregleda> TerminPregleda { get; set; }
        public DbSet<ElektronskiKarton> ElektronskiKarton { get; set; }
        public DbSet<PregledVida> PregledVida { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Korisnik>().ToTable("Korisnik");
            modelBuilder.Entity<Proizvod>().ToTable("Proizvod");
            modelBuilder.Entity<Korpa>().ToTable("Korpa");
            modelBuilder.Entity<StavkaKorpe>().ToTable("StavkaKorpe");
            modelBuilder.Entity<Narudzba>().ToTable("Narudzba");
            modelBuilder.Entity<Placanje>().ToTable("Placanje");
            modelBuilder.Entity<Obavijest>().ToTable("Obavijest");
            modelBuilder.Entity<Poslovnica>().ToTable("Poslovnica");
            modelBuilder.Entity<TerminPregleda>().ToTable("TerminPregleda");
            modelBuilder.Entity<ElektronskiKarton>().ToTable("ElektronskiKarton");
            modelBuilder.Entity<PregledVida>().ToTable("PregledVida");

            modelBuilder.Entity<Narudzba>()
       .HasOne(n => n.Korisnik)
       .WithMany()
       .HasForeignKey(n => n.IdKorisnik)
       .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Narudzba>()
                .HasOne(n => n.Korpa)
                .WithMany()
                .HasForeignKey(n => n.IdKorpa)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}