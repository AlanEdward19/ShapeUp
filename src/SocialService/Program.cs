using System.Globalization;
using ServiceDefaults;
using SharedKernel.Utils;
using SocialService.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost8080",
        builder => builder.WithOrigins("http://localhost:8080")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

AuthenticationUtils.GetIssuerSigningKey(configuration);
builder.AddServiceDefaults();
builder.Services.SolveServiceDependencies(configuration);
builder.Services.ConfigureEndpoints();

var cultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var app = builder.Build();

// Use CORS policy
app.UseCors("AllowLocalhost8080");

app.ConfigureSwagger();
app.MapEndpoints(configuration);
app.ConfigureMiddleware();

app.Run();
app.Logger.LogInformation("Application instance is ready to handle incoming requests");