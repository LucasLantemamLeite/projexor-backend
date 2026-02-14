using App.Middlewares;

namespace App.Extensions.Config;

public static partial class Inject
{
    extension(WebApplication app)
    {
        public WebApplication ApplyAppConfig()
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UsePathBase("/v1");

            app.UseHealthChecks("/health");

            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();

            return app;
        }
    }
}