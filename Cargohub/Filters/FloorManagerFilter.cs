using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cargohub.Filters;

public class FloorManagerFilter : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext actionContext, ActionExecutionDelegate next)
    {
        var context = actionContext.HttpContext;

        if (!context.Request.Headers.ContainsKey("API_key"))
        {
            context.Response.StatusCode = 401;
            return;
        }

        if (context.Request.Headers["API_key"] != "i9j10k11")
        {
            context.Response.StatusCode = 401;
            return;
        }
        await next.Invoke();

    }
}