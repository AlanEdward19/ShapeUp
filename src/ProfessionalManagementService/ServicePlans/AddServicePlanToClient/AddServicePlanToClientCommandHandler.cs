using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Clients;
using ProfessionalManagementService.Common.Enums;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Connections.Database;
using SharedKernel.Exceptions;

namespace ProfessionalManagementService.ServicePlans.AddServicePlanToClient;

public class AddServicePlanToClientCommandHandler(DatabaseContext dbContext)
    : IHandler<ClientDto, AddServicePlanToClientCommand>
{
    public async Task<ClientDto> HandleAsync(AddServicePlanToClientCommand command, CancellationToken cancellationToken)
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

        if (!client.Id.Equals(command.LoggedInUserId))
            throw new UnauthorizedAccessException("You do not have permission to add this service plan to the client.");

        if (client.ClientServicePlans.Any(x => x.ServicePlanId == command.ServicePlanId))
            throw new InvalidOperationException(
                $"Client with Id: '{command.ClientId}' already has ServicePlan with Id: '{command.ServicePlanId}'.");

        var strategy = dbContext.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                ClientServicePlan clientServicePlan = new(servicePlan.Id, DateTime.Today,
                    DateTime.Today.AddDays(servicePlan.DurationInDays), ESubscriptionStatus.Active, null);

                clientServicePlan.SetServicePlan(servicePlan);
                client.AddServicePlan(clientServicePlan);

                dbContext.Clients.Update(client);
                await dbContext.ClientServicePlans.AddAsync(clientServicePlan, cancellationToken);

                await dbContext.SaveChangesAsync(cancellationToken);
                await dbContext.Database.CommitTransactionAsync(cancellationToken);

                return new ClientDto(client);
            }
            catch (Exception e)
            {
                await dbContext.Database.RollbackTransactionAsync(cancellationToken);
                throw new InvalidOperationException(
                    $"An error occurred while adding ServicePlan to Client: {e.Message}", e);
            }
        });
    }
}