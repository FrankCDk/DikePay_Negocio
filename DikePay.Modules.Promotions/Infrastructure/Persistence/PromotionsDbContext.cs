using DikePay.Modules.Promotions.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DikePay.Modules.Promotions.Infrastructure.Persistence
{
    public class PromotionsDbContext : DbContext
    {
        public PromotionsDbContext(DbContextOptions<PromotionsDbContext> options) : base(options)
        {
        }

        public DbSet<Promotion> Promotions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PromotionsDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
