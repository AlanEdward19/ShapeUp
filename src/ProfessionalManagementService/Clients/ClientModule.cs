using ProfessionalManagementService.Clients.CreateClient;
using ProfessionalManagementService.Clients.DeleteClient;
using ProfessionalManagementService.Clients.GetClientById;
using ProfessionalManagementService.Clients.GetClientsByProfessionalId;
using ProfessionalManagementService.Clients.UpdateClient;
using ProfessionalManagementService.Common.Interfaces;

namespace ProfessionalManagementService.Clients;

public static class ClientModule
{
    public static IServiceCollection AddClientServices(this IServiceCollection services)
    {
        services
            .AddHandlers();
        
        return services;
    }
    
    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<ClientDto, CreateClientCommand>, CreateClientCommandHandler>();
        services.AddScoped<IHandler<ClientDto, UpdateClientCommand>, UpdateClientCommandHandler>();
        services.AddScoped<IHandler<ClientDto, GetClientByIdQuery>, GetClientByIdQueryHandler>();
        services.AddScoped<IHandler<List<ClientDto>, GetClientsByProfessionalIdQuery>, GetClientsByProfessionalIdQueryHandler>();
        services.AddScoped<IHandler<bool, DeleteClientCommand>, DeleteClientCommandHandler>();

        return services;
    }
}