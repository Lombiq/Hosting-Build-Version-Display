using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Layout;

namespace Lombiq.Hosting.BuildVersionDisplay.Filters;

internal class BuildVersionDisplayInjectingFilter : IAsyncResultFilter
{
    private readonly ILayoutAccessor _layoutAccessor;
    private readonly IShapeFactory _shapeFactory;
    private readonly IOptions<AdminOptions> _adminOptions;

    public BuildVersionDisplayInjectingFilter(
        ILayoutAccessor layoutAccessor,
        IShapeFactory shapeFactory,
        IOptions<AdminOptions> adminOptions)
    {
        _layoutAccessor = layoutAccessor;
        _shapeFactory = shapeFactory;
        _adminOptions = adminOptions;
    }

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.IsNotFullViewRendering() ||
            !context.IsAdmin() ||
            !context.HttpContext.Request.Path.ToString().TrimEnd('/').EqualsOrdinalIgnoreCase("/" + _adminOptions.Value.AdminUrlPrefix))
        {
            await next();

            return;
        }

        var layout = await _layoutAccessor.GetLayoutAsync();
        var zone = layout.Zones["Content"];
        await zone.AddAsync(await _shapeFactory.CreateAsync("BuildVersion"), ":after");

        await next();
    }
}
