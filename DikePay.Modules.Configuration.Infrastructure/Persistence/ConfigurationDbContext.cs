using DikePay.Modules.Configuration.Domain;
using Microsoft.EntityFrameworkCore;

namespace DikePay.Modules.Configuration.Infrastructure.Persistence
{
    public class ConfigurationDbContext : DbContext
    {
        public ConfigurationDbContext(DbContextOptions<ConfigurationDbContext> options) : base(options) { }

        public DbSet<AppVersion> AppVersions { get; set; }
        public DbSet<GlobalSetting> GlobalSettings { get; set; }
        public DbSet<AppReleaseNotes> AppReleaseNotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. Configuración de AppVersion
            modelBuilder.Entity<AppVersion>(entity => {
                entity.ToTable("app_versions");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id").HasColumnType("char(36)");
                entity.Property(e => e.Platform).HasColumnName("platform");
                entity.Property(e => e.VersionNumber).HasColumnName("version_number").HasMaxLength(20);
                entity.Property(e => e.BuildNumber).HasColumnName("build_number");
                entity.Property(e => e.IsCriticalUpdate).HasColumnName("is_critical_update").HasColumnType("tinyint(1)");
                entity.Property(e => e.ReleaseDate).HasColumnName("release_date").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.DownloadUrl).HasColumnName("download_url").HasMaxLength(255);
                entity.Property(e => e.IsActive).HasColumnName("is_active").HasColumnType("tinyint(1)").HasDefaultValue(true);

                // Relación 1:N con AppReleaseNotes
                entity.HasMany(v => v.ReleaseNotes)
                      .WithOne()
                      .HasForeignKey(n => n.AppVersionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // 2. Configuración de AppReleaseNotes
            modelBuilder.Entity<AppReleaseNotes>(entity => {
                entity.ToTable("app_release_notes");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id").HasColumnType("char(36)");
                entity.Property(e => e.AppVersionId).HasColumnName("app_version_id").HasColumnType("char(36)");
                entity.Property(e => e.LanguageCode).HasColumnName("language_code").HasMaxLength(5);
                entity.Property(e => e.Notes).HasColumnName("notes").HasColumnType("text");
            });

            // 3. Configuración de GlobalSetting
            modelBuilder.Entity<GlobalSetting>(entity => {
                entity.ToTable("global_settings");
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key).HasColumnName("config_key").HasMaxLength(100);
                entity.Property(e => e.Value).HasColumnName("config_value").HasColumnType("text");
                entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(255);
                entity.Property(e => e.LastUpdated).HasColumnName("last_updated")
                      .ValueGeneratedOnAddOrUpdate()
                      .HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");
            });
        }
    }
}