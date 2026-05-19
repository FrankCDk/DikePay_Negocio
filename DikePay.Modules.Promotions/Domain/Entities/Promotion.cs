namespace DikePay.Modules.Promotions.Domain.Entities
{
    public class Promotion
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // ID Técnico (PK)
        public string CodigoPromocion { get; set; } = string.Empty; // ID de Negocio (Ej: PROM-002-PERC)
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string TipoPromocion { get; set; } = "CANTIDAD_FIJA";
        public Guid ArticuloId { get; set; }
        public decimal CantidadMinima { get; set; } = 1;
        public decimal? NuevoPrecio { get; set; }
        public decimal? PorcentajeDescuento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Estado { get; set; } = "V";
    }
}
