using DikePay.Modules.Configuracion.Domain;
using Microsoft.EntityFrameworkCore;

namespace DikePay.Modules.Configuracion.Infrastructure.Persistence
{
    public class ConfigurationDbContext : DbContext
    {
        public ConfigurationDbContext(DbContextOptions<ConfigurationDbContext> options) : base(options) { }

        public DbSet<VersionApp> VersionApp { get; set; }
        public DbSet<ConfiguracionGlobal> ConfiguracionesGlobales { get; set; }
        public DbSet<NotasLanzamiento> NotasLanzamiento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. Configuración de VersionApp
            modelBuilder.Entity<VersionApp>(entity => {
                entity.ToTable("versiones_app");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id").HasColumnType("char(36)");
                entity.Property(e => e.Plataforma).HasColumnName("plataforma");
                entity.Property(e => e.NumeroVersion).HasColumnName("numero_version").HasMaxLength(20);
                entity.Property(e => e.NumeroBuild).HasColumnName("numero_build");
                entity.Property(e => e.EsActualizacionCritica).HasColumnName("es_actualizacion_critica").HasColumnType("tinyint(1)");
                entity.Property(e => e.FechaLanzamiento).HasColumnName("fecha_lanzamiento").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UrlDescarga).HasColumnName("url_descarga").HasMaxLength(255);
                entity.Property(e => e.EsActiva).HasColumnName("es_activa").HasColumnType("tinyint(1)").HasDefaultValue(true);

                entity.HasMany(v => v.Notas)
                      .WithOne()
                      .HasForeignKey(n => n.IdVersionApp)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // 2. Configuración de NotasLanzamiento
            modelBuilder.Entity<NotasLanzamiento>(entity => {
                entity.ToTable("notas_lanzamiento");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id").HasColumnType("char(36)");
                entity.Property(e => e.IdVersionApp).HasColumnName("id_version_app").HasColumnType("char(36)");
                entity.Property(e => e.CodigoIdioma).HasColumnName("codigo_idioma").HasMaxLength(5);
                entity.Property(e => e.Notas).HasColumnName("notas").HasColumnType("text");
            });

            // 3. Configuración de ConfiguracionGlobal
            modelBuilder.Entity<ConfiguracionGlobal>(entity => {
                entity.ToTable("configuracion_global");
                entity.HasKey(e => e.Clave);

                entity.Property(e => e.Clave).HasColumnName("clave_configuracion").HasMaxLength(100);
                entity.Property(e => e.Valor).HasColumnName("valor_configuracion").HasColumnType("text");
                entity.Property(e => e.Descripcion).HasColumnName("descripcion").HasMaxLength(255);
                entity.Property(e => e.FechaActualizacion).HasColumnName("fecha_actualizacion")
                      .ValueGeneratedOnAddOrUpdate()
                      .HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");
            });
        }
    }
}