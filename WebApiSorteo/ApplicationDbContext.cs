using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApiSorteo.Entidades;

namespace WebApiSorteo
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ParticipantesCartasSorteo>()
            .HasKey(r => new { r.IdSorteo, r.IdParticipantes, r.IdCartas });
        }

        public DbSet<Participantes> Participantes { get; set; }
        public DbSet<Sorteo> Sorteo { get; set; }
        public DbSet<Premios> Premios { get; set; }
        public DbSet<Cartas> Cartas { get; set; }
      public DbSet<ParticipantesCartasSorteo> ParticipantesCartasSorteo { get; set; }
    }
}
