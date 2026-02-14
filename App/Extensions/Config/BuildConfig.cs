using App.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace App.Extensions.Config;

public static partial class Inject
{
    extension(WebApplicationBuilder builder)
    {
        public WebApplicationBuilder ApplyBuildConfig(IConfiguration configuration)
        {
            var conn = configuration.GetConnectionString("Default");

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(conn));

            return builder;
        }
    }
}