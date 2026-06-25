namespace DikePay.Modules.Articulos.Domain
{
    public class Articulo
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Version { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string CodigoSku { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;        
        public string? CodigoProductoSunat { get; set; }
        public string UnidadMedida { get; set; } = string.Empty;
        public string? TipoArticulo { get; set; }
        public string? TipoExistenciaSunat { get; set; }

        /// <summary>
        /// Flags
        /// </summary>
        public bool AceptaDecimales { get; set; }
        public bool TieneSerie { get; set; }
        public bool TieneLote { get; set; }
        public bool ControlaStock { get; set; }
        public bool EsPrecioLibre { get; set; }


        /// <summary>
        /// IMPORTES
        /// </summary>
        public string Moneda { get; set; } = string.Empty;
        public decimal PorcentajeDescuento { get; set; }
        public string TipoAfectacion { get; set; } = string.Empty;        
        public decimal Precio { get; set; }
        public decimal StockMinimo { get; set; }

        /// <summary>
        /// AUDITORIA
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; } = string.Empty;
        public DateTime? FechaActualizacion { get; set; }
        public string? UsuarioActualizacion { get; set; }
    }

}