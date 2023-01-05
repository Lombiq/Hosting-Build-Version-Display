using Atata;
using Lombiq.Tests.UI.Extensions;
using Lombiq.Tests.UI.Services;
using OpenQA.Selenium;
using Shouldly;
using System.Threading.Tasks;

namespace Lombiq.Hosting.BuildVersionDisplay.Tests.UI.Extensions;

public static class TestCaseUITestContextExtensions
{
    /// <summary>
    /// Tests the Lombiq Hosting - Build Version Display for Orchard Core feature.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <paramref name="checkBuildLink"/> defaults to false since the build link won't be displayed when the tests are
    /// run from a published UI test project (it displays properly when run from source). This is because the UI test
    /// project doesn't (shouldn't) reference the module project/package directly and thus won't use its targets file
    /// either, breaking the BuildVersionDisplay_BuildUrl property. This is not an error, the link is still displayed
    /// when the web app is run directly.
    /// </para>
    /// </remarks>
    public static async Task TestBuildVersionDisplayAsync(this UITestContext context, bool checkBuildLink = false)
    {
        await context.SignInDirectlyAndGoToDashboardAsync();

        var container = context.Get(By.Id("lombiq-hosting-build-version-display-build-version"));
        var listItems = container.GetAll(By.TagName("li"));

        var orchardVersion = typeof(OrchardCore.IOrchardHelper).Assembly.GetName().Version.ToString();
        listItems.ShouldContain(element => element.Text == "Orchard Core version: " + orchardVersion);
        listItems.ShouldContain(element => element.Text.Contains("App build version"));
        if (checkBuildLink)
        {
            listItems.ShouldContain(element => element.Text.Contains("Build link"));
        }
    }
}
