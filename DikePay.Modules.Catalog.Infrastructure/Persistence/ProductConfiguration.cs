using DikePay.Modules.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DikePay.Modules.Catalog.Infrastructure.Persistence
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // 1. Nombre de la tabla
            builder.ToTable("articulos");

            // 2. Llave primaria (GUID)
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                   .HasColumnName("id")
                   .ValueGeneratedNever(); // El GUID lo generamos en C# (Offline-ready)

            // 3. Identidad de Negocio e Índices
            builder.HasIndex(p => p.Code)
                   .IsUnique()
                   .HasDatabaseName("idx_articulos_codigo");

            builder.Property(p => p.Code)
                   .HasColumnName("codigo")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(p => p.Sku)
                   .HasColumnName("codigo_sku")
                   .HasMaxLength(50);

            builder.Property(p => p.Version)
                   .HasColumnName("version")
                   .HasMaxLength(20)
                   .HasDefaultValue("1.0.0");

            builder.Property(p => p.Name)
                   .HasColumnName("nombre")
                   .HasMaxLength(200)
                   .IsRequired();

            // 4. Campos Numéricos (Precios y Stocks)
            // Usamos precisión de 18,4 para evitar errores de redondeo tributarios
            builder.Property(p => p.Price)
                   .HasColumnName("precio")
                   .HasPrecision(18, 4);

            builder.Property(p => p.Stock)
                   .HasColumnName("stock")
                   .HasPrecision(18, 4);

            builder.Property(p => p.StockMin)
                   .HasColumnName("stock_minimo")
                   .HasPrecision(18, 4);

            builder.Property(p => p.DiscountPercentage)
                   .HasColumnName("porcentaje_descuento")
                   .HasPrecision(5, 2);

            // 5. Clasificaciones y SUNAT
            builder.Property(p => p.TaxProductCode)
                   .HasColumnName("codigo_producto_sunat")
                   .HasMaxLength(20);

            builder.Property(p => p.Unit)
                   .HasColumnName("unidad_medida")
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(p => p.ProductType)
                   .HasColumnName("tipo_articulo")
                   .HasMaxLength(50);

            builder.Property(p => p.TaxInventoryType)
                   .HasColumnName("tipo_existencia_sunat")
                   .HasMaxLength(10);

            // 6. Flags (Booleanos mapeados a TinyInt en MySQL)
            builder.Property(p => p.AllowsDecimals).HasColumnName("acepta_decimales");
            builder.Property(p => p.HasSerialNumber).HasColumnName("tiene_serie");
            builder.Property(p => p.HasBatchNumber).HasColumnName("tiene_lote");
            builder.Property(p => p.TrackStock).HasColumnName("controla_stock");
            builder.Property(p => p.IsOpenPrice).HasColumnName("es_precio_libre");

            // 7. Otros datos de negocio
            builder.Property(p => p.Currency)
                   .HasColumnName("moneda")
                   .HasMaxLength(5)
                   .HasDefaultValue("MN");

            builder.Property(p => p.TaxAffectationType)
                   .HasColumnName("tipo_afectacion")
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(p => p.Status)
                   .HasColumnName("estado")
                   .HasMaxLength(1)
                   .IsFixedLength() // CHAR(1)
                   .IsRequired();

            // 8. Auditoría
            builder.Property(p => p.CreatedAt).HasColumnName("fecha_creacion");
            builder.Property(p => p.UserCreatedAt).HasColumnName("usuario_creacion").HasMaxLength(50);
            builder.Property(p => p.UpdatedAt).HasColumnName("fecha_modificacion");
            builder.Property(p => p.UserUpdateddAt).HasColumnName("usuario_modificacion").HasMaxLength(50);
        }
    }
}
