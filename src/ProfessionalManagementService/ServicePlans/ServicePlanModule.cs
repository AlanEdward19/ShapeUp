using ProfessionalManagementService.Clients;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.ServicePlans.AddServicePlanToClient;
using ProfessionalManagementService.ServicePlans.CreateServicePlan;
using ProfessionalManagementService.ServicePlans.DeleteServicePlan;
using ProfessionalManagementService.ServicePlans.GetServicePlanById;
using ProfessionalManagementService.ServicePlans.GetServicePlansByProfessionalId;
using ProfessionalManagementService.ServicePlans.UpdateServicePlan;

namespace ProfessionalManagementService.ServicePlans;

/// <summary>
/// Módulo de configuração dos serviços relacionados aos planos de serviço
/// </summary>
public static class ServicePlanModule
{
    public static IServiceCollection ConfigureServicePlanServices(this IServiceCollection services)
    {
        services
            .AddHandlers();
        
        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<ServicePlanDto, CreateServicePlanCommand>, CreateServicePlanCommandHandler>();
        services.AddScoped<IHandler<ClientDto, AddServicePlanToClientCommand>, AddServicePlanToClientCommandHandler>();
        services.AddScoped<IHandler<ServicePlanDto, UpdateServicePlanCommand>, UpdateServicePlanCommandHandler>();
        services.AddScoped<IHandler<List<ServicePlanDto>, GetServicePlansByProfessionalIdQuery>, GetServicePlansByProfessionalIdQueryHandler>();
        services.AddScoped<IHandler<ServicePlanDto, GetServicePlanByIdQuery>, GetServicePlanByIdQueryHandler>();
        services.AddScoped<IHandler<bool, DeleteServicePlanCommand>, DeleteServicePlanCommandHandler>();
        
        return services;
    }
}