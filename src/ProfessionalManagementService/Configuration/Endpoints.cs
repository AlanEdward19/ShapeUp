using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using Asp.Versioning;
using Microsoft.OpenApi.Models;
using ProfessionalManagementService.Configuration.Filters;

namespace ProfessionalManagementService.Configuration;

/// <summary>
///     Configuração dos Endpoints
/// </summary>
public static class EndpointsConfiguration
{
    /// <summary>
    ///     Mapeia os endpoint.
    /// </summary>
    /// <param name="app">The app.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>An IEndpointRouteBuilder.</returns>
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app, IConfiguration configuration)
    {
        app.MapControllers();
        
        return app;
    }

    /// <summary>
    ///     Configura os endpoints.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureEndpoints(this IServiceCollection services)
    {
        #region API Versioning

        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
            options.AssumeDefaultVersionWhenUnspecified = true;
        });

        #endregion

        #region API Docs

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "ShapeUp",
                Version = "v1",
                Description = """
                              ShapeUp é uma rede social inovadora para praticantes de atividades físicas, conectando usuários a profissionais de saúde, oferecendo gerenciamento de treinos e nutrição, marketplace de serviços, e um ambiente colaborativo para motivação e evolução.
                              """,
                Contact = new OpenApiContact
                {
                    Name = "Alan Oliveira",
                    Email = "alan-edward@outlook.com.br",
                    Url = new Uri("https://github.com/AlanEdward19")
                }
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Por favor insira o token JWT com o prefixo 'Bearer '",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });

            #region Servers

            c.AddServer(new OpenApiServer
            {
                Url = "http://localhost:5000",
                Description = "Local"
            });

            #endregion

            c.CustomSchemaIds(x =>
                x.GetCustomAttributes(false).OfType<DisplayNameAttribute>().FirstOrDefault()?.DisplayName ?? x.Name);

#if DEBUG
            Directory.GetFiles("./Configuration/Comments/", "*.xml", SearchOption.TopDirectoryOnly).ToList()
                .ForEach(xml => c.IncludeXmlComments(xml));
#endif

            c.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
            c.OperationFilter<VersionHeaderFilter>();
        });

        #endregion

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

        services.AddHealthChecks();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}