using App.Extensions.Config;

var builder = WebApplication.CreateBuilder(args);

builder.ApplyBuildConfig(builder.Configuration);

var app = builder.Build();

app.ApplyAppConfig();

app.Run();
