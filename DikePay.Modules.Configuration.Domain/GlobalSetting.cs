namespace DikePay.Modules.Configuration.Domain
{
    public class GlobalSetting
    {
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
