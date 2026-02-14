namespace App.Middlewares;

public sealed class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }

        catch (OperationCanceledException) { }

        catch (Exception ex)
        {
            logger.LogError(ex, "Exception threw.");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var message = new { message = "Erro interno no servidor, tente novamente mais tarde." };

            await context.Response.WriteAsJsonAsync(message);
        }
    }
}