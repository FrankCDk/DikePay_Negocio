using FluentValidation;
using MediatR;

namespace DikePay.Shared.Infrastructure.Behaviors
{
    /// <summary>
    /// Intercepta la tubería de MediatR para ejecutar todos los validadores de FluentValidation
    /// asociados a un Command o Query antes de que el Handler sea invocado.
    /// </summary>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        // Inyección de todos los validadores de FluentValidation para el tipo TRequest actual.
        // El DI (Dependency Injection) inyecta aquí una lista de 0 a N validadores.
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request,
                                            RequestHandlerDelegate<TResponse> next,
                                            CancellationToken cancellationToken)
        {
            // 1. Verificar si existen validadores registrados para esta solicitud
            if (!_validators.Any())
            {
                // Si no hay validadores, continúa directamente al siguiente paso (Handler).
                return await next();
            }

            // 2. Crear el contexto de validación
            var context = new ValidationContext<TRequest>(request);

            // 3. Ejecutar todos los validadores de forma asíncrona
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken))
            );

            // 4. Recopilar todos los errores de todos los resultados de validación
            var failures = validationResults
                .Where(r => r.Errors.Any()) // Solo resultados con errores
                .SelectMany(r => r.Errors)  // Aplanar la lista de errores
                .ToList();

            // 5. Decidir si hay fallos y lanzar la excepción
            if (failures.Any())
            {
                // Lanza la excepción de FluentValidation.
                // Esta excepción debe ser capturada por tu Middleware de Manejo de Excepciones
                // en la capa de API para generar la respuesta HTTP 400.
                throw new ValidationException(failures);
            }

            // 6. Si no hay fallos, el Command/Query es válido, procede al Handler
            return await next();
        }
    }
}
