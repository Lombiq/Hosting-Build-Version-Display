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
