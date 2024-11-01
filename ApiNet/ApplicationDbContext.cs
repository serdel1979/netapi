using ApiNet.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiNet
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        public DbSet<Equipo> Equipos { get; set; }
    }
}
