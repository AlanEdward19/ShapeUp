using ProfessionalManagementService.Clients;
using ProfessionalManagementService.Connections;
using ProfessionalManagementService.Professionals;
using ProfessionalManagementService.Reviews;
using ProfessionalManagementService.ServicePlans;

namespace ProfessionalManagementService.Configuration;

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
            .ConfigureProfessionalServices()
            .ConfigureServicePlanServices()
            .ConfigureReviewServices()
            .AddClientServices()
            .ConfigureConnections(configuration);

        return services;
    }
}