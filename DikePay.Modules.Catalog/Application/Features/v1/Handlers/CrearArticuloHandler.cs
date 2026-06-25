using DikePay.Modules.Articulos.Application.Abstractions.Persistence;
using DikePay.Modules.Articulos.Domain;
using DikePay.Modules.Articulos.Shared.Contracts.v1.Commands;
using DikePay.Shared.Models;
using MediatR;

namespace DikePay.Modules.Articulos.Application.Features.v1.Handlers
{
    public class CrearArticuloHandler : IRequestHandler<CrearArticuloCommand, ApiResponse>
    {
        private readonly IArticuloUnitOfWork _unitOfWork;
        private readonly IArticuloRepository _articuloRepository;
        public CrearArticuloHandler(IArticuloUnitOfWork unitOfWork, IArticuloRepository articuloRepository)
        {
            _articuloRepository = articuloRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse> Handle(CrearArticuloCommand request, CancellationToken cancellationToken)
        {
            // Validamos si existe el articulo con el mismo codigo
            if (await _articuloRepository.ExistePorCodigoAsync(request.Codigo, cancellationToken))
            {
                throw new Exception("El producto con el código especificado ya existe en el sistema");
            }

            // Validamos si existe un articulo con el mismo codigo SKU
            if (await _articuloRepository.ExistePorSkuAsync(request.CodigoSku, request.Id, cancellationToken))
            {
                throw new Exception("El producto con el SKU especificado ya existe en el sistema");
            }

            var articulo = new Articulo
            {
                // Lógica: Si el Id enviado es Guid.Empty, generamos uno nuevo.
                Id = (request.Id == Guid.Empty) ? Guid.NewGuid() : request.Id,

                Version = Guid.NewGuid().ToString().Substring(0, 8),
                Codigo = request.Codigo,
                Estado = request.Estado,
                CodigoSku = request.CodigoSku,
                Nombre = request.Nombre,

                // Mapeo de campos SUNAT
                CodigoProductoSunat = request.CodigoProductoSunat ?? string.Empty,
                UnidadMedida = request.UnidadMedida,
                TipoArticulo = request.TipoArticulo ?? string.Empty,
                TipoExistenciaSunat = request.TipoExistenciaSunat ?? string.Empty,

                // Flags de control
                AceptaDecimales = request.AceptaDecimales,
                TieneSerie = request.TieneSerie,
                TieneLote = request.TieneLote,
                ControlaStock = request.ControlaStock,
                EsPrecioLibre = request.EsPrecioLibre,

                // Importes y Finanzas
                Moneda = request.Moneda,
                PorcentajeDescuento = request.PorcentajeDescuento,
                TipoAfectacion = request.TipoAfectacion ?? "GR",
                Precio = request.Precio,
                StockMinimo = request.StockMinimo,

                // Auditoría
                FechaCreacion = DateTime.UtcNow,
                UsuarioCreacion = "API_USER",
            };

            await _articuloRepository.CrearAsync(articulo, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ApiResponse.Ok( "Articulo creado exitosamente en el servidor");

        }
    }
}