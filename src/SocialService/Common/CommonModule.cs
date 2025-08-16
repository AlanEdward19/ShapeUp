using Refit;
using SocialService.Common.Services;
using SocialService.Common.Services.BrasilApi;

namespace SocialService.Common;

/// <summary>
///    Módulo para configuração de dependências comuns.
/// </summary>
public static class CommonModule
{
    /// <summary>
    /// Método para resolver as dependências comuns.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureCommonRelatedDependencies(this IServiceCollection services)
    {
        services
            .AddExternalApis();

        return services;
    }

    private static IServiceCollection AddExternalApis(this IServiceCollection services)
    {
        services.AddRefitClient<IBrasilApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://cep.awesomeapi.com.br"));
        
        return services;
    }
}