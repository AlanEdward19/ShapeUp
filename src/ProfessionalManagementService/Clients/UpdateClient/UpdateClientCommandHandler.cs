using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Connections.Database;
using SharedKernel.Exceptions;

namespace ProfessionalManagementService.Clients.UpdateClient;

public class UpdateClientCommandHandler(DatabaseContext dbContext) : IHandler<ClientDto, UpdateClientCommand>
{
    public async Task<ClientDto> HandleAsync(UpdateClientCommand command, CancellationToken cancellationToken)
    {
        var client = await
            dbContext.Clients
                .AsSplitQuery()
                .Include(x => x.ClientServicePlans)
                .ThenInclude(x => x.ServicePlan)
                .Include(x => x.ClientProfessionalReviews)
                .FirstOrDefaultAsync(x => x.Id == command.GetId(), cancellationToken);
        
        if(client == null)
            throw new NotFoundException($"Client with Id: '{command.GetId()}' not found.");
        
        await dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            client.UpdateEmail(command.Email);
            dbContext.Clients.Update(client);
            await dbContext.SaveChangesAsync(cancellationToken);
            await dbContext.Database.CommitTransactionAsync(cancellationToken);

            return new ClientDto(client);
        }
        catch (Exception)
        {
            await dbContext.Database.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}