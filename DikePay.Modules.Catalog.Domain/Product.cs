namespace DikePay.Modules.Catalog.Domain
{
    public class Product
    {

        /// <summary>
        /// Identificador único.
        /// Generado en el dispositivo de origen para evitar colisiones durante la sincronización.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Versión del producto. Lo usaremos para que al momento de sincronizar ambos deben estar en la misma versión.
        /// </summary>
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// Código único del producto.
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Código SKU (Stock Keeping Unit).
        /// Usado para el código de barras u otros identificadores de inventario.
        /// </summary>
        public string? Sku { get; set; } = string.Empty;

        /// <summary>
        /// Nombre del producto.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Precio del producto
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Stock del producto
        /// </summary>
        public decimal Stock { get; set; }

        /// <summary>
        /// Stock minimo del producto
        /// </summary>
        public decimal StockMin { get; set; }

        /// <summary>
        /// Código del producto SUNAT
        /// </summary>
        public string TaxProductCode { get; set; } = string.Empty;

        /// <summary>
        /// Unidad de medida del producto
        /// </summary>
        public string Unit { get; set; } = string.Empty;

        /// <summary>
        /// Tipo de producto
        /// </summary>
        public string ProductType { get; set; } = string.Empty;

        /// <summary>
        /// Tipo de producto SUNAT
        /// </summary>
        public string TaxInventoryType { get; set; } = string.Empty;

        /// <summary>
        /// Flag para indicar si el producto acepta decimales en stock
        /// </summary>
        public bool AllowsDecimals { get; set; }

        /// <summary>
        /// Flag para indicar si el producto usa serie.
        /// </summary>
        public bool HasSerialNumber { get; set; }

        /// <summary>
        /// Flag para indicar si el producto usa lote.
        /// </summary>
        public bool HasBatchNumber { get; set; }

        /// <summary>
        /// Flag para indicar si se controlara el stock del producto
        /// </summary>
        public bool TrackStock { get; set; }

        /// <summary>
        /// Flag para indicar si el producto tiene un precio que se puede modificar al realizar la facturación.
        /// </summary>
        public bool IsOpenPrice { get; set; }

        /// <summary>
        /// Moneda del producto
        /// </summary>
        public string Currency { get; set; } = string.Empty;

        /// <summary>
        /// Porcentaje de descuento del producto
        /// </summary>
        public decimal DiscountPercentage { get; set; }

        /// <summary>
        /// Tipo de afectación del impuesto: GR (Gravado), IN (Inafecto) o EX (Exonerado)
        /// </summary>
        public string TaxAffectationType { get; set; } = string.Empty;

        /// <summary>
        /// Estado del producto: V (Vigente), A (Anulado) o B (Bloqueado).
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Fecha de creación
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Usuario de creación
        /// </summary>
        public string UserCreatedAt { get; set; } = string.Empty;

        /// <summary>
        /// Fecha de actualización
        /// </summary>
        public DateTime UpdatedAt { get; set; } // FechaModificacion

        /// <summary>
        /// Usuario de actualización
        /// </summary>
        public string UserUpdateddAt { get; set; } = string.Empty;
    }
}