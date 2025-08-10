using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Clients;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Connections.Database;
using SharedKernel.Exceptions;

namespace ProfessionalManagementService.ServicePlans.DeleteServicePlanFromClient;

public class DeleteServicePlanFromClientCommandHandler(DatabaseContext dbContext) : IHandler<ClientDto, DeleteServicePlanFromClientCommand>
{
    public async Task<ClientDto> HandleAsync(DeleteServicePlanFromClientCommand command, CancellationToken cancellationToken)
    {
        var client = await dbContext.Clients
            .Include(x => x.ClientServicePlans)
            .ThenInclude(x => x.ServicePlan)
            .FirstOrDefaultAsync(x => x.Id == command.ClientId, cancellationToken);
        
        if (client == null)
            throw new NotFoundException($"Client with Id: '{command.ClientId}' not found.");
        
        var servicePlan = await dbContext.ServicePlans
            .FirstOrDefaultAsync(x => x.Id == command.ServicePlanId, cancellationToken);
        
        if (servicePlan == null)
            throw new NotFoundException($"ServicePlan with Id: '{command.ServicePlanId}' not found.");
        
        if(!client.Id.Equals(command.LoggedInUserId) && !servicePlan.ProfessionalId.Equals(command.LoggedInUserId))
            throw new UnauthorizedAccessException("You do not have permission to remove this service plan from the client.");
        
        if (client.ClientServicePlans.All(x => x.ServicePlanId != command.ServicePlanId))
            throw new InvalidOperationException($"Client with Id: '{command.ClientId}' doesn't have ServicePlan with Id: '{command.ServicePlanId}'.");
        
        await dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            ClientServicePlan clientServicePlan = client.ClientServicePlans.First(x => x.ServicePlanId == command.ServicePlanId);
            client.RemoveServicePlan(clientServicePlan);
            
            dbContext.Clients.Update(client);
            
            await dbContext.SaveChangesAsync(cancellationToken);
            await dbContext.Database.CommitTransactionAsync(cancellationToken);
            
            return new ClientDto(client);
        }
        catch (Exception e)
        {
            await dbContext.Database.RollbackTransactionAsync(cancellationToken);
            throw new InvalidOperationException($"An error occurred while removing ServicePlan from Client: {e.Message}", e);
        }
    }
}