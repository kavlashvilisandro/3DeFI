using _3DeFI.API.Domain;
namespace _3DeFI.API.Presentation;

public class GlobalErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    public GlobalErrorHandlerMiddleware(RequestDelegate next)
    {
        this._next = next;
    }

    public async Task InvokeAsync(HttpContext  context)
    {
        try
        {
            await _next(context);
        }
        catch (BaseResponseException ex)
        {
            context.Response.StatusCode = ex.StatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new { ErrorMessage = ex.Message });
        }
    }
}