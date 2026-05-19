using DikePay.Modules.Configuration.Domain.Enums;

namespace DikePay.Modules.Configuration.Domain
{
    public class AppVersion
    {
        public Guid Id { get; set; } = new Guid();
        public AppPlatform Platform { get; set; }
        public string VersionNumber { get; set; } = string.Empty;
        public int BuildNumber { get; set; }
        public bool IsCriticalUpdate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? DownloadUrl { get; set; }
        public bool IsActive { get; set; }

        // Propiedad de navegación para las notas
        public ICollection<AppReleaseNotes> ReleaseNotes { get; set; } = new List<AppReleaseNotes>();
    }
}
