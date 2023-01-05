namespace Lombiq.Hosting.BuildVersionDisplay;

[AttributeUsage(AttributeTargets.Assembly)]
internal sealed class BuildUrlAttribute : Attribute
{
    public string? Url { get; }

    public BuildUrlAttribute(string? url) => Url = url;
}
