using ProfessionalManagementService.Clients;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Scores.GetProfessionalScoreByProfessionalId;
using ProfessionalManagementService.ServicePlans;
using ProfessionalManagementService.ServicePlans.AddServicePlanToClient;
using ProfessionalManagementService.ServicePlans.CreateServicePlan;
using ProfessionalManagementService.ServicePlans.DeleteServicePlan;
using ProfessionalManagementService.ServicePlans.GetServicePlanById;
using ProfessionalManagementService.ServicePlans.GetServicePlansByProfessionalId;
using ProfessionalManagementService.ServicePlans.UpdateServicePlan;

namespace ProfessionalManagementService.Scores;

/// <summary>
/// Módulo de configuração dos serviços relacionados a pontuação do professional
/// </summary>
public static class ScoreModule
{
    public static IServiceCollection ConfigureScoreServices(this IServiceCollection services)
    {
        services
            .AddHandlers();
        
        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<ProfessionalScore, GetProfessionalScoreByProfessionalIdQuery>,GetProfessionalScoreByProfessionalIdQueryHandler>();
        
        return services;
    }
}