using Microsoft.EntityFrameworkCore;

namespace FabricaNube.Data
{
    public class FabricaDbContext : DbContext
    {
        public FabricaDbContext(DbContextOptions<FabricaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Presupuesto> Presupuestos { get; set; }
        public DbSet<LoteProduccion> LotesProduccion { get; set; }
        public DbSet<SolicitudLeche> SolicitudesLeche { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}


