using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cargohub.Filters;

public class ManagerFilter : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext actionContext, ActionExecutionDelegate next)
    {
        var context = actionContext.HttpContext;

        if (!context.Request.Headers.ContainsKey("API_key"))
        {
            context.Response.StatusCode = 401;
            return;
        }

        if (context.Request.Headers["API_key"] != "e5f6g7h8" && context.Request.Headers["API_key"] != "a1b2c3d4")
        {
            context.Response.StatusCode = 401;
            return;
        }
        await next.Invoke();

    }
}