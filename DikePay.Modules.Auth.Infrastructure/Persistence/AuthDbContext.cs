using DikePay.Modules.Auth.Domain;
using Microsoft.EntityFrameworkCore;

namespace DikePay.Modules.Auth.Infrastructure.Persistence
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        public DbSet<UserAccount> Users => Set<UserAccount>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>(builder =>
            {
                builder.ToTable("usuarios");
                builder.HasKey(u => u.Code);
                builder.Property(u => u.Code).HasColumnName("codigo_usuario");
                builder.Property(u => u.Name).HasColumnName("nombre_completo");
                builder.Property(u => u.Email).HasColumnName("email");
                builder.Property(u => u.PasswordHash).HasColumnName("password_hash");
                builder.Property(u => u.Role).HasColumnName("rol");
                builder.Property(u => u.IsActive).HasColumnName("estado");
            });
        }
    }
}
