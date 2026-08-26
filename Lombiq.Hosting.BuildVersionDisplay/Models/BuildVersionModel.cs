using System.Reflection;

namespace Lombiq.Hosting.BuildVersionDisplay.Models;

public class BuildVersionModel
{
    private static readonly Lazy<(Version OrchardVersion, Version BuildVersion, string? BuildUrl)> _defaults = new(() =>
    {
        var webAppAssembly = Assembly.GetEntryAssembly();

        // These can't be null as long as the code actually compiles.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var orchardVersion = typeof(OrchardCore.IOrchardHelper).Assembly.GetName().Version;
        var buildVersion = webAppAssembly.GetName().Version;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        // When using the module from NuGet the web app's binaries will contain the attribute, when using it from
        // source, the module's.
        var webAppAttribute = webAppAssembly.GetCustomAttribute<BuildUrlAttribute>();
        var buildUrl = webAppAttribute?.Url;

        return (orchardVersion!, buildVersion!, buildUrl);
    });

    public static Version AssemblyBuildVersion => _defaults.Value.BuildVersion;

    public string OrchardVersion { get; set; }
    public string BuildVersion { get; set; }
    public string? BuildUrl { get; set; }

    public BuildVersionModel()
    {
        var (orchardVersion, buildVersion, buildUrl) = _defaults.Value;

        if (string.IsNullOrEmpty(buildUrl))
        {
            // Needed so it works in both NuGet and source.
#pragma warning disable S3902 // S3902: Replace this call to 'Assembly.GetExecutingAssembly()' with 'Type.Assembly'.
            var moduleAttribute = Assembly.GetExecutingAssembly().GetCustomAttribute<BuildUrlAttribute>();
#pragma warning restore S3902
            buildUrl = moduleAttribute?.Url;
        }

        OrchardVersion = orchardVersion.ToString();
        BuildVersion = buildVersion.ToString();
        BuildUrl = buildUrl;
    }
}
