using DikePay.Modules.Catalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace DikePay.Modules.Catalog.Infrastructure.Persistence
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aquí le decimos a EF que este módulo solo debe mirar sus tablas
            // y cómo se mapean a la DB existente
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

        }
    }
}
