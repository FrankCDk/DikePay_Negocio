using DikePay.Modules.Almacenes.Domain;
using Microsoft.EntityFrameworkCore;

namespace DikePay.Modules.Almacenes.Infrastructure.Persistence
{
    public class AlmacenDbContext : DbContext
    {
        public AlmacenDbContext(DbContextOptions<AlmacenDbContext> options ) : base(options)
        {
            
        }

        public DbSet<Almacen> Warehouses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AlmacenDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
