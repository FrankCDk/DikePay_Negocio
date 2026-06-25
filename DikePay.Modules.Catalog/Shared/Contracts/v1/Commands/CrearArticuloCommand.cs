using DikePay.Shared.Models;
using MediatR;

namespace DikePay.Modules.Articulos.Shared.Contracts.v1.Commands
{
    // Cambiamos el tipo de retorno en el IRequest de Guid a ProductResponse
    public record CrearArticuloCommand(
        Guid Id,
        string Version,
        string Codigo,
        string Estado,
        string CodigoSku,
        string Nombre,
        string? CodigoProductoSunat,
        string UnidadMedida,
        string? TipoArticulo,
        string? TipoExistenciaSunat,

        // Flags
        bool AceptaDecimales,
        bool TieneSerie,
        bool TieneLote,
        bool ControlaStock,
        bool EsPrecioLibre,

        // Importes
        string Moneda,
        decimal PorcentajeDescuento,
        string TipoAfectacion,
        decimal Precio,
        decimal StockMinimo
    ) : IRequest<ApiResponse>;
}