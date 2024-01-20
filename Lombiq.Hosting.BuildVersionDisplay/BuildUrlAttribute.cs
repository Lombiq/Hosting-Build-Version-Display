namespace Lombiq.Hosting.BuildVersionDisplay;

[AttributeUsage(AttributeTargets.Assembly)]
public sealed class BuildUrlAttribute(string? url) : Attribute
{
    public string? Url { get; } = url;
}
