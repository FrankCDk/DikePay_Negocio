using DikePay.Modules.Articulos.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DikePay.Modules.Articulos.Infrastructure.Persistence
{
    public class ArticuloConfiguration : IEntityTypeConfiguration<Articulo>
    {
        public void Configure(EntityTypeBuilder<Articulo> builder)
        {
            // 1. Nombre de la tabla
            builder.ToTable("articulos");

            // 2. Llave primaria (GUID)
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                   .HasColumnName("id")
                   .HasColumnType("char(36)")
                   .ValueGeneratedNever(); // El GUID lo generamos en C# (Offline-ready)

            // 3. Identidad de Negocio e Índices
            builder.HasIndex(p => p.Codigo)
                   .IsUnique()
                   .HasDatabaseName("idx_articulos_codigo");

            builder.Property(p => p.Codigo)
                   .HasColumnName("codigo")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(p => p.CodigoSku)
                   .HasColumnName("codigo_sku")
                   .HasMaxLength(50);

            builder.Property(p => p.Version)
                   .HasColumnName("version")
                   .HasMaxLength(20)
                   .HasDefaultValue("1.0.0");

            builder.Property(p => p.Nombre)
                   .HasColumnName("nombre")
                   .HasMaxLength(200)
                   .IsRequired();

            // 4. Campos Numéricos (Precios y Stocks)
            // Usamos precisión de 18,4 para evitar errores de redondeo tributarios
            builder.Property(p => p.Precio)
                   .HasColumnName("precio")
                   .HasPrecision(18, 4);

            builder.Property(p => p.StockMinimo)
                   .HasColumnName("stock_minimo")
                   .HasPrecision(18, 4);

            builder.Property(p => p.PorcentajeDescuento)
                   .HasColumnName("porcentaje_descuento")
                   .HasPrecision(5, 2);

            // 5. Clasificaciones y SUNAT
            builder.Property(p => p.CodigoProductoSunat)
                   .HasColumnName("codigo_producto_sunat")
                   .HasMaxLength(20);

            builder.Property(p => p.UnidadMedida)
                   .HasColumnName("unidad_medida")
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(p => p.TipoArticulo)
                   .HasColumnName("tipo_articulo")
                   .HasMaxLength(50);

            builder.Property(p => p.TipoExistenciaSunat)
                   .HasColumnName("tipo_existencia_sunat")
                   .HasMaxLength(10);

            // 6. Flags (Booleanos mapeados a TinyInt en MySQL)
            builder.Property(p => p.AceptaDecimales).HasColumnName("acepta_decimales");
            builder.Property(p => p.TieneSerie).HasColumnName("tiene_serie");
            builder.Property(p => p.TieneLote).HasColumnName("tiene_lote");
            builder.Property(p => p.ControlaStock).HasColumnName("controla_stock");
            builder.Property(p => p.EsPrecioLibre).HasColumnName("es_precio_libre");

            // 7. Otros datos de negocio
            builder.Property(p => p.Moneda)
                   .HasColumnName("moneda")
                   .HasMaxLength(5)
                   .HasDefaultValue("MN");

            builder.Property(p => p.TipoAfectacion)
                   .HasColumnName("tipo_afectacion")
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(p => p.Estado)
                   .HasColumnName("estado")
                   .HasMaxLength(1)
                   .IsFixedLength()
                   .IsRequired();

            // 8. Auditoría
            builder.Property(p => p.FechaCreacion).HasColumnName("fecha_creacion");
            builder.Property(p => p.UsuarioCreacion).HasColumnName("usuario_creacion").HasMaxLength(50);
            builder.Property(p => p.FechaActualizacion).HasColumnName("fecha_modificacion").IsRequired(false);
            builder.Property(p => p.UsuarioActualizacion).HasColumnName("usuario_modificacion").HasMaxLength(50).IsRequired(false);
        }
    }
}
