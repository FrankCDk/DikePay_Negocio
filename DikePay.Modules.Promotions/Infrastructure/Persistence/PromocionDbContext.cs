using DikePay.Modules.Promociones.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DikePay.Modules.Promociones.Infrastructure.Persistence
{
    public class PromocionDbContext : DbContext
    {
        public PromocionDbContext(DbContextOptions<PromocionDbContext> options) : base(options)
        {
        }

        public DbSet<Promocion> Promociones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PromocionDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
