using System.Net;
using System.Text.Json;

namespace MarketPlace.Web.Infastructure.Middleware;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {

            await HandelException(context, ex);
        }
    }

    private async Task HandelException(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var error = new { Message = ex.Message };

        var jsonError = JsonSerializer.Serialize(error);

        await context.Response.WriteAsync(jsonError);
    }
}
