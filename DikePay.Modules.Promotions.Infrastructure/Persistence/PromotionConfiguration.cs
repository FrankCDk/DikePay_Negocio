using DikePay.Modules.Promotions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DikePay.Modules.Promotions.Infrastructure.Persistence
{
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {

        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("promociones"); // Nombre de tu tabla en MySQL

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                    .HasColumnName("id")
                    .HasColumnType("char(36)") // Especificamos el tipo de DB
                    .ValueGeneratedNever();

            builder.Property(p => p.ArticuloId)
                .HasColumnName("articulo_id")
                .HasColumnType("char(36)") // Especificamos el tipo de DB
                .IsRequired();


            // Código de Negocio (Donde guardarás "PROM-002-PERC")
            builder.Property(p => p.CodigoPromocion)
                .HasColumnName("codigo_promocion")
                .HasMaxLength(50)
                .IsRequired();

            // Opcional: Crear un índice único para que no se repitan códigos de promo
            builder.HasIndex(p => p.CodigoPromocion).IsUnique();

            builder.Property(p => p.Nombre)
                .HasColumnName("nombre")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Descripcion)
                .HasColumnName("descripcion")
                .HasMaxLength(255);

            builder.Property(p => p.TipoPromocion)
                .HasColumnName("tipo_promocion")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.CantidadMinima)
                .HasColumnName("cantidad_minima")
                .HasPrecision(18, 4);

            builder.Property(p => p.NuevoPrecio)
                .HasColumnName("nuevo_precio")
                .HasPrecision(18, 4);

            builder.Property(p => p.PorcentajeDescuento)
                .HasColumnName("porcentaje_descuento")
                .HasPrecision(18, 2);

            builder.Property(p => p.FechaInicio)
                .HasColumnName("fecha_inicio");

            builder.Property(p => p.FechaFin)
                .HasColumnName("fecha_fin");

            builder.Property(p => p.Estado)
                .HasColumnName("estado")
                .HasMaxLength(1)
                .HasDefaultValue("V");
        }
    }
}
