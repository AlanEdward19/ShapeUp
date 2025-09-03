using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Connections.Database;
using SharedKernel.Exceptions;

namespace ProfessionalManagementService.Clients.DeleteClient;

public class DeleteClientCommandHandler(DatabaseContext dbContext) : IHandler<bool, DeleteClientCommand>
{
    public async Task<bool> HandleAsync(DeleteClientCommand command, CancellationToken cancellationToken)
    {
        var client = await dbContext.Clients.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (client == null)
            throw new NotFoundException($"Client with Id: '{command.Id}' not found.");

        var strategy = dbContext.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                dbContext.Clients.Remove(client);
                await dbContext.SaveChangesAsync(cancellationToken);
                await dbContext.Database.CommitTransactionAsync(cancellationToken);

                return true;
            }
            catch (Exception)
            {
                await dbContext.Database.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        });
    }
}