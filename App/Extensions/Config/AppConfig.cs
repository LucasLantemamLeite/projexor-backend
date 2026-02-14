namespace App.Extensions.Config;

public static partial class Inject
{
    extension(WebApplication app)
    {
        public WebApplication ApplyAppConfig()
        {
            return app;
        }
    }
}