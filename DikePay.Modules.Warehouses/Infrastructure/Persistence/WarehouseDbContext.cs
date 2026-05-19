using DikePay.Modules.Warehouses.Domain;
using Microsoft.EntityFrameworkCore;

namespace DikePay.Modules.Warehouses.Infrastructure.Persistence
{
    public class WarehouseDbContext : DbContext
    {
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options ) : base(options)
        {
            
        }

        public DbSet<Warehouse> Warehouses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WarehouseDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
