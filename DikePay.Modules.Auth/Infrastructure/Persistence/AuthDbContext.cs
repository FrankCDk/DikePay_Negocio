using DikePay.Modules.Auth.Domain;
using Microsoft.EntityFrameworkCore;

namespace DikePay.Modules.Auth.Infrastructure.Persistence
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        public DbSet<UserAccount> Users => Set<UserAccount>();
        public DbSet<MobileAuthCode> MobileAuthCodes => Set<MobileAuthCode>();

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

            modelBuilder.Entity<MobileAuthCode>(entity =>
            {
                entity.ToTable("codigos_autenticacion_movil"); // Nombre de la nueva tabla
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Code).HasMaxLength(10).IsRequired();
                entity.Property(c => c.Id).HasColumnName("id");
                entity.Property(c => c.UserId).HasColumnName("codigo_usuario");
                entity.Property(c => c.Code).HasColumnName("codigo_autenticacion");
                entity.Property(c => c.IsUsed).HasColumnName("es_usado");
                entity.Property(c => c.ExpiryDate).HasColumnName("fecha_expiracion");

                // Relación con tu tabla existente
                entity.HasOne(d => d.User)
                      .WithMany() // Un usuario puede tener muchos intentos de login QR
                      .HasForeignKey(d => d.UserId)
                      .HasPrincipalKey(u => u.Code); // Apuntamos a 'codigo_usuario'
            });

        }
    }
}
