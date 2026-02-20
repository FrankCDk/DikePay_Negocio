using DikePay.Modules.Warehouses.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DikePay.Modules.Warehouses.Infrastructure.Persistence
{
    /// <summary>
    /// Clase de configuración de la relacion de la entidad con la tabla de la base de datos.
    /// Aquí es donde se definen las claves primarias, relaciones, índices, etc.
    /// </summary>
    public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.ToTable("almacenes");

            builder.HasKey(w => w.Id);
            builder.Property(w => w.Id)
                .HasColumnName("id")
                .HasColumnType("char(36)")
                .ValueGeneratedNever();


            builder.Property(w => w.Code)
                .HasColumnName("codigo")
                .HasColumnType("char(4)")
                .IsRequired();

            // Indice del Código del almacén para garantizar su unicidad
            builder.HasIndex(w => w.Code)
                .IsUnique()
                .HasDatabaseName("idx_almacenes_codigo");

            builder.Property(w => w.Description)
                .HasColumnName("descripcion")
                .HasColumnType("varchar(80)")
                .IsRequired();

            // Indice de la Descripción del almacén para garantizar su unicidad
            builder.HasIndex(w => w.Description)
                .IsUnique()
                .HasDatabaseName("idx_almacenes_descripcion");

            builder.Property(w => w.IsActive)
                    .HasColumnName("activo")
                    .HasColumnType("tinyint(1)") // Esto es lo que MySQL usa para Boolean
                    .HasDefaultValue(true)       // Coincide con tu DEFAULT TRUE de la DB
                    .IsRequired();

            builder.Property(w => w.CreatedAt)
                .HasColumnName("fecha_creacion")
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .HasColumnName("fecha_creacion")
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Property(p => p.CreatedBy).HasColumnName("usuario_creacion").HasMaxLength(50);

            builder.Property(w => w.UpdatedAt)
                .HasColumnName("fecha_modificacion")
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Property(p => p.UpdatedBy).HasColumnName("usuario_modificacion").HasMaxLength(50);

        }
    }
}
