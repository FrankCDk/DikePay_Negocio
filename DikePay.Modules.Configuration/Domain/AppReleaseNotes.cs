namespace DikePay.Modules.Configuration.Domain
{
    public class AppReleaseNotes
    {
        public Guid Id { get; set; }
        public Guid AppVersionId { get; set; }
        public string LanguageCode { get; set; } = "es";
        public string Notes { get; set; } = string.Empty;
    }
}
