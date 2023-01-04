using Lombiq.Tests.UI.Services;
using System.Threading.Tasks;

namespace Lombiq.Hosting.BuildVersionDisplay.Tests.UI.Extensions;

public static class TestCaseUITestContextExtensions
{
    /// <summary>
    /// Tests the Lombiq Hosting - Build Version Display for Orchard Core feature.
    /// </summary>
    public static Task TestBuildVersionDisplayAsync(this UITestContext context) => Task.CompletedTask;
}
