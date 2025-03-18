using AuthService.Authentication;
using AuthService.Common;
using AuthService.Connections;
using AuthService.Group;
using AuthService.Permission;

namespace AuthService.Configuration;

/// <summary>
///     Classe para resolver as dependências de serviços
/// </summary>
public static class ServiceDependencies
{
    /// <summary>
    ///     Método para resolver as dependências de serviços
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection SolveServiceDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .ConfigureCommonRelatedDependencies()
            .ConfigureConnections(configuration)
            .ConfigureAuthenticationRelatedDependencies()
            .ConfigureGroupRelatedDependencies()
            .ConfigurePermissionRelatedDependencies();

        return services;
    }
}