using DikePay.Modules.Articulos.Domain;
using Microsoft.EntityFrameworkCore;

namespace DikePay.Modules.Articulos.Infrastructure.Persistence
{
    public class ArticuloDbContext : DbContext
    {
        public ArticuloDbContext(DbContextOptions<ArticuloDbContext> options) : base(options) { }

        public DbSet<Articulo> Articulos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aquí le decimos a EF que este módulo solo debe mirar sus tablas
            // y cómo se mapean a la DB existente
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArticuloDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

        }
    }
}
