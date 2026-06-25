namespace DikePay.Modules.Almacenes.Domain
{
    public class Almacen
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Ubigeo { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime UpdatedAt { get; set;}
        public string UpdatedBy { get; set; } = string.Empty;
    }
}
