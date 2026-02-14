using App.Middlewares;

namespace App.Extensions.Config;

public static partial class Inject
{
    extension(WebApplication app)
    {
        public WebApplication ApplyAppConfig()
        {
            app.UseMiddleware<ExceptionMiddleware>();

            return app;
        }
    }
}