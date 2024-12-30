using System.Globalization;
using ChatService.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add CORS policy to allow specific origins and enable credentials
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder => builder.WithOrigins("http://localhost:8080")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

builder.Services.SolveServiceDependencies(configuration);
builder.Services.ConfigureAuthentication(configuration);
builder.Services.ConfigureEndpoints();

var cultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var app = builder.Build();

// Use CORS policy
app.UseCors("AllowSpecificOrigins");

app.ConfigureSwagger();
app.MapEndpoints(configuration);
app.ConfigureMiddleware();

app.Run();
app.Logger.LogInformation("Application instance is ready to handle incoming requests");