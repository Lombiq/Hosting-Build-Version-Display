using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Layout;
using OrchardCore.Modules;

namespace Lombiq.Hosting.BuildVersionDisplay.Filters;

internal sealed class BuildVersionDisplayInjectingFilter(
    ILayoutAccessor layoutAccessor,
    IShapeFactory shapeFactory,
    IOptions<AdminOptions> adminOptions) : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.IsNotFullViewRendering() ||
            !context.IsAdmin() ||
            !context.HttpContext.Request.Path.ToString().TrimEnd('/').EqualsOrdinalIgnoreCase("/" + adminOptions.Value.AdminUrlPrefix))
        {
            await next();

            return;
        }

        var layout = await layoutAccessor.GetLayoutAsync();
        var zone = layout.Zones["Content"];
        await zone.AddAsync(await shapeFactory.CreateAsync("BuildVersion"), ":after");

        await next();
    }
}
