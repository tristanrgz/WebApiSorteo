using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApiSorteo.Entidades;

namespace WebApiSorteo
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Participantes> Participantes { get; set; }
    }
}
