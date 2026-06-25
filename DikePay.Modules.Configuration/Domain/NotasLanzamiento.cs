namespace DikePay.Modules.Configuracion.Domain
{
    public class NotasLanzamiento
    {
        public Guid Id { get; set; }
        public Guid IdVersionApp { get; set; } // Ajustado para coincidir con el nombre de la FK
        public string CodigoIdioma { get; set; } = "es";
        public string Notas { get; set; } = string.Empty;
    }
}
