using System.Globalization;
using ServiceDefaults;
using SharedKernel.Utils;
using TrainingService.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

AuthenticationUtils.GetIssuerSigningKey(configuration);
builder.AddServiceDefaults();
builder.Services.SolveServiceDependencies(configuration);
builder.Services.ConfigureAuthentication(configuration);
builder.Services.ConfigureEndpoints();

var cultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var app = builder.Build();

app.ConfigureSwagger();
app.MapEndpoints(configuration);
app.ConfigureMiddleware();
app.UpdateMigrations();

app.Run();
app.Logger.LogInformation("Application instance is ready to handle incoming requests");