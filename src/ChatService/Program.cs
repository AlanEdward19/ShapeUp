using System.Globalization;
using ChatService.Chat;
using ChatService.Configuration;
using ServiceDefaults;
using SharedKernel.Utils;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddHealthChecks();

// Add CORS policy to allow specific origins and enable credentials
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder => builder.WithOrigins("http://localhost:8080", "https://www.shapeup.cloud", "https://shapeup.cloud")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

AuthenticationUtils.GetIssuerSigningKey(configuration);
builder.AddServiceDefaults();
builder.Services.SolveServiceDependencies(configuration);
builder.Services.ConfigureEndpoints();

var cultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var app = builder.Build();

app.MapHealthChecks("/healthz");
app.MapGet("/", () => Results.Ok("OK"));

// Use CORS policy
app.UseCors("AllowSpecificOrigins");

app.MapHub<ChatHub>("/chat");

app.ConfigureSwagger();
app.MapEndpoints(configuration);
app.ConfigureMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
app.Logger.LogInformation("Application instance is ready to handle incoming requests");