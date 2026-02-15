using System.Text;
using App.Data.Context;
using App.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace App.Extensions.Config;

public static partial class Inject
{
    extension(WebApplicationBuilder builder)
    {
        public WebApplicationBuilder ApplyBuildConfig(IConfiguration configuration)
        {
            var conn = configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(conn));

            var key = configuration.GetValue<string>("JwtKey") ?? throw new NullReferenceException("JwtKey não encontrada.");
            JwtToken.SetKey(key);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
                options.TokenValidationParameters = new()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtToken.Key)),
                    RequireAudience = true,
                    ValidateAudience = true,
                    ValidAudience = "ProjexorClient",
                    ValidateIssuer = true,
                    ValidIssuer = "ProjexorServer",
                    ValidAlgorithms = [SecurityAlgorithms.HmacSha256],
                    ValidTypes = ["JWT"],
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(15)
                }
            );

            builder.Services.AddAuthorization();

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