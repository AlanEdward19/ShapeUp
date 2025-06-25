using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Connections.Database;
using ProfessionalManagementService.Professionals.CreateProfessional;
using SharedKernel.Exceptions;

namespace ProfessionalManagementService.Professionals.DeleteProfessional;

public class DeleteProfessionalCommandHandler(DatabaseContext dbContext)
    : IHandler<bool, DeleteProfessionalCommand>
{
    public async Task<bool> HandleAsync(DeleteProfessionalCommand command,
        CancellationToken cancellationToken)
    {
        var professional = await dbContext.Professionals.FirstOrDefaultAsync(x=> x.Id == command.Id, cancellationToken);
        
        if (professional == null)
            throw new NotFoundException($"Professional with Id: '{command.Id}' not found.");

        await dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            dbContext.Professionals.Remove(professional);
            await dbContext.SaveChangesAsync(cancellationToken);
            await dbContext.Database.CommitTransactionAsync(cancellationToken);

            return true;
        }
        catch (Exception)
        {
            await dbContext.Database.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}