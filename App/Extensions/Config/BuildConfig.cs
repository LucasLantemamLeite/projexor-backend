using App.Data.Context;
using Microsoft.AspNetCore.Mvc;
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

            builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
            }

            builder.Services.AddHealthChecks();

            builder.Services.AddControllers();

            return builder;
        }
    }
}