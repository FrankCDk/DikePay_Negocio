namespace DikePay.Modules.Configuracion.Application.Contracts.v1.DTOs
{
    public record RespuestaVerificacionVersion(
        bool ActualizacionDisponible,
        bool EsCritica,
        string NumeroVersionActual,
        int NumeroBuildActual,
        string? UrlDescarga,
        List<string> NotasLanzamiento
    );
}
