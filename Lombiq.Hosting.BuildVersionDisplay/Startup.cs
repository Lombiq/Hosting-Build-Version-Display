using Lombiq.Hosting.BuildVersionDisplay.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;

namespace Lombiq.Hosting.BuildVersionDisplay;

public class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services) =>
        services.Configure<MvcOptions>((options) => options.Filters.Add(typeof(BuildVersionDisplayInjectingFilter)));
}
