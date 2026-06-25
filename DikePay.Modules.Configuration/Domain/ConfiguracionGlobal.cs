namespace DikePay.Modules.Configuracion.Domain
{
    public class ConfiguracionGlobal
    {
        public string Clave { get; set; } = string.Empty;
        public string Valor { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
