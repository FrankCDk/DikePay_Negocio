using DikePay.Modules.Configuration.Shared.Contracts.v1.Commands;
using FluentValidation;

namespace DikePay.Modules.Configuration.Application.Features.v1.Versions.Validators
{
    public class CreateVersionValidator : AbstractValidator<CreateVersionCommand>
    {

        public CreateVersionValidator()
        {
            // Validación de Plataforma (Enum)
            RuleFor(x => x.Platform)
                .IsInEnum().WithMessage("La plataforma seleccionada no es válida (1: Android, 2: iOS, 3: Windows).");

            // Versión (Texto x.x.x)
            RuleFor(x => x.VersionNumber)
                .NotEmpty().WithMessage("El número de versión es obligatorio.")
                .MaximumLength(20).WithMessage("La versión no puede superar los 20 caracteres.")
                .Matches(@"^\d+\.\d+\.\d+$").WithMessage("El formato debe ser semántico (ejemplo: 1.0.4).");

            // Build Number (Entero incremental)
            RuleFor(x => x.BuildNumber)
                .GreaterThan(0).WithMessage("El número de compilación (Build) debe ser mayor a 0.");

            // Download URL (Link a la tienda)
            RuleFor(x => x.DownloadUrl)
                .MaximumLength(255).WithMessage("La URL es demasiado larga.")
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(x => !string.IsNullOrEmpty(x.DownloadUrl))
                .WithMessage("La URL de descarga no tiene un formato válido.");

            // Flags Booleanos (Asegurar que no vengan nulos si usas tipos anulables)
            RuleFor(x => x.IsCritical)
                .NotNull().WithMessage("Debes especificar si es una actualización crítica.");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("Debes especificar si la versión está activa.");
        }
    }
}
