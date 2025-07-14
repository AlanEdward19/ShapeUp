using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Professionals.CreateProfessional;
using ProfessionalManagementService.Professionals.DeleteProfessional;
using ProfessionalManagementService.Professionals.GetProfessionalById;
using ProfessionalManagementService.Professionals.GetProfessionals;
using ProfessionalManagementService.Professionals.UpdateProfessional;

namespace ProfessionalManagementService.Professionals;

/// <summary>
/// Módulo de configuração dos serviços relacionados aos profissionais
/// </summary>
public static class ProfessionalModule
{
    /// <summary>
    /// Método de extensão para configurar os serviços relacionados aos profissionais
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureProfessionalServices(this IServiceCollection services)
    {
        services
            .AddHandlers();
        
        return services;
    }
    
    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<ProfessionalDto, CreateProfessionalCommand>, CreateProfessionalCommandHandler>();
        services.AddScoped<IHandler<ProfessionalDto, UpdateProfessionalCommand>, UpdateProfessionalCommandHandler>();
        services.AddScoped<IHandler<ProfessionalDto, GetProfessionalByIdQuery>, GetProfessionalByIdQueryHandler>();
        services.AddScoped<IHandler<List<ProfessionalDto>, GetProfessionalsQuery>, GetProfessionalsQueryHandler>();
        services.AddScoped<IHandler<bool, DeleteProfessionalCommand>, DeleteProfessionalCommandHandler>();
        
        return services;
    }
}