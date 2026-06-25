using DikePay.Modules.Articulos.Application.Abstractions.Persistence;
using DikePay.Modules.Articulos.Shared.Contracts.v1.DTOs;
using DikePay.Modules.Articulos.Shared.Contracts.v1.Queries;
using DikePay.Shared.Models;
using MediatR;

namespace DikePay.Modules.Articulos.Application.Features.v1.Handlers
{
    public class ListarArticulosHandler : IRequestHandler<ListarArticulosQuery, ApiResponse<IEnumerable<ArticuloResponse>>>
    {

        private readonly IArticuloRepository _articuloRepository;
        public ListarArticulosHandler(IArticuloRepository articuloRepository)
        {
            _articuloRepository = articuloRepository;
        }

        public async Task<ApiResponse<IEnumerable<ArticuloResponse>>> Handle(ListarArticulosQuery request, CancellationToken cancellationToken)
        {

            var products = await _articuloRepository.ListarAsync(cancellationToken);

            // Mapeamos de Entidad -> DTO
            var productsResponse = products.Select(p => new ArticuloResponse(
                    p.Id,
                    p.Version,
                    p.Codigo,
                    p.CodigoSku,
                    p.Nombre,
                    p.Precio,
                    p.StockMinimo,
                    p.CodigoProductoSunat,
                    p.UnidadMedida,
                    p.TipoArticulo,
                    p.TipoExistenciaSunat,
                    p.AceptaDecimales,
                    p.TieneSerie,
                    p.TieneLote,
                    p.ControlaStock,
                    p.EsPrecioLibre,
                    p.Moneda,
                    p.PorcentajeDescuento,
                    p.TipoAfectacion,
                    p.Estado,
                    p.FechaCreacion,
                    p.UsuarioCreacion,
                    p.FechaActualizacion,
                    p.UsuarioActualizacion
                ));
            
            return ApiResponse<IEnumerable<ArticuloResponse>>.Ok(productsResponse);
        }
    }
}
