using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Connections.Database;
using SharedKernel.Exceptions;

namespace ProfessionalManagementService.Professionals.CreateProfessional;

public class CreateProfessionalCommandHandler(DatabaseContext dbContext)
    : IHandler<ProfessionalDto, CreateProfessionalCommand>
{
    public async Task<ProfessionalDto> HandleAsync(CreateProfessionalCommand command,
        CancellationToken cancellationToken)
    {
        var client = await dbContext.Clients.AsNoTracking().FirstOrDefaultAsync(x => x.Id ==command.GetId(), cancellationToken);
        
        if (client == null)
            throw new NotFoundException($"User with Id: '{command.GetId()}' not found.");
        
        command.SetEmail(client.Email);
        command.SetName(client.Name);
        
        var professional = command.ToProfessional();
        
        var strategy = dbContext.Database.CreateExecutionStrategy();
        
        return await strategy.ExecuteAsync(async () =>
        {
            await dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await dbContext.Professionals.AddAsync(professional, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                await dbContext.Database.CommitTransactionAsync(cancellationToken);

                return new ProfessionalDto(professional);
            }
            catch (Exception)
            {
                await dbContext.Database.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        });
    }
}