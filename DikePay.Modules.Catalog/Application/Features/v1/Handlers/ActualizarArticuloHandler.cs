using DikePay.Modules.Articulos.Application.Abstractions.Persistence;
using DikePay.Modules.Articulos.Domain;
using DikePay.Modules.Articulos.Shared.Contracts.v1.Commands;
using DikePay.Shared.Models;
using MediatR;

namespace DikePay.Modules.Articulos.Application.Features.v1.Handlers
{
    public class ActualizarArticuloHandler : IRequestHandler<ActualizarArticuloCommand, ApiResponse>
    {
        private readonly IArticuloUnitOfWork _unitOfWork;
        private readonly IArticuloRepository _articuloRepository;

        public ActualizarArticuloHandler(IArticuloUnitOfWork unitOfWork, IArticuloRepository articuloRepository)
        {
            _unitOfWork = unitOfWork;
            _articuloRepository = articuloRepository;
        }

        public async Task<ApiResponse> Handle(ActualizarArticuloCommand request, CancellationToken cancellationToken)
        {
            // 1. Obtener el producto actual de la BD para comparar versiones
            var productoExistente = await _articuloRepository.ObtenerPorIdAsync(request.Id, cancellationToken) ?? throw new Exception("El producto no existe.");

            // 2. VALIDACIÓN DE CONCURRENCIA OPTIMISTA
            if (productoExistente.Version != request.Version)
            {
                // Esto le dice al Middleware que lance un 409 Conflict
                throw new ConcurrencyException("El registro fue modificado por otro usuario. Por favor, actualice sus datos.");
            }

            if (await _articuloRepository.ExistePorSkuAsync(request.CodigoSku, request.Id, cancellationToken))
            {
                throw new Exception("El SKU ya está siendo utilizado por otro producto.");
            }

            // 1. Creamos la entidad de producto
            var articulo = new Articulo
            {
                Id = request.Id,
                Version = Guid.NewGuid().ToString().Substring(0, 8),
                Codigo = request.Codigo,
                Estado = request.Estado,
                CodigoSku = request.CodigoSku,
                Nombre = request.Nombre,
                CodigoProductoSunat = request.CodigoProductoSunat ?? string.Empty,
                UnidadMedida = request.UnidadMedida,
                TipoArticulo = request.TipoArticulo ?? string.Empty,
                TipoExistenciaSunat = request.TipoExistenciaSunat ?? string.Empty,

                // Flags
                AceptaDecimales = request.AceptaDecimales,
                TieneSerie = request.TieneSerie,
                TieneLote = request.TieneLote,
                ControlaStock = request.ControlaStock,
                EsPrecioLibre = request.EsPrecioLibre,

                // Importes
                Moneda = request.Moneda,
                PorcentajeDescuento = request.PorcentajeDescuento,
                TipoAfectacion = request.TipoAfectacion ?? "GR",
                Precio = request.Precio,
                StockMinimo = request.StockMinimo,

                // Auditoría
                FechaActualizacion = DateTime.UtcNow,
                UsuarioActualizacion = "API_USER"
            };

            // 2. Actualizamos el producto en la base de datos
            await _articuloRepository.ActualizarAsync(articulo);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ApiResponse.Ok("Producto actualizado exitosamente en el servidor");
        }
    }
}
