namespace DikePay.Modules.Configuration.Shared.Contracts.v1.DTOs
{
    public record VersionCheckResponse(
        bool UpdateAvailable,
        bool IsCritical,
        string LatestVersionNumber,
        int LatestBuildNumber,
        string? DownloadUrl,
        List<string> ReleaseNotes
    );
}
