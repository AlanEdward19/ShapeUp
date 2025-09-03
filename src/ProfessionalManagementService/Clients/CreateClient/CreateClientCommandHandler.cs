using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Connections.Database;

namespace ProfessionalManagementService.Clients.CreateClient;

public class CreateClientCommandHandler(DatabaseContext dbContext) : IHandler<ClientDto, CreateClientCommand>
{
    public async Task<ClientDto> HandleAsync(CreateClientCommand command, CancellationToken cancellationToken)
    {
        var client = command.ToClient();
        
        var strategy = dbContext.Database.CreateExecutionStrategy();
        
        return await strategy.ExecuteAsync(async () =>
        {
            await dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await dbContext.Clients.AddAsync(client, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                await dbContext.Database.CommitTransactionAsync(cancellationToken);

                return new ClientDto(client);
            }
            catch (Exception)
            {
                await dbContext.Database.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        });
    }
}