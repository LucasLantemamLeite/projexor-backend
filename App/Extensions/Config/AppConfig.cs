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

            app.UseAuthentication();

            app.UseAuthorization();

            app.UsePathBase("/v1");

            app.UseHealthChecks("/v1/health");

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