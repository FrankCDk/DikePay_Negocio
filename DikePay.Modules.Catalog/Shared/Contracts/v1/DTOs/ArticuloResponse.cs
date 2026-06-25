namespace DikePay.Modules.Articulos.Shared.Contracts.v1.DTOs
{
    public record ArticuloResponse(
        Guid Id,
        string Version,
        string Codigo,
        string CodigoSku,
        string Nombre,
        decimal Precio,
        decimal StockMinimo,
        string? CodigoProductoSunat,
        string UnidadMedida,
        string? TipoArticulo,
        string? TipoExistenciaSunat,
        bool AceptaDecimales,
        bool TieneSerie,
        bool TieneLote,
        bool ControlaStock,
        bool EsPrecioLibre,
        string Moneda,
        decimal PorcentajeDescuento,
        string TipoAfectacion,
        string Estado,
        DateTime FechaCreacion,
        string UsuarioCreacion,
        DateTime? FechaActualizacion,
        string? UsuarioActualizacion
    );
}