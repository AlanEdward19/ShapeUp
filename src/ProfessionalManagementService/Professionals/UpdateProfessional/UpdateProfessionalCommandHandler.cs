using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Connections.Database;

namespace ProfessionalManagementService.Professionals.UpdateProfessional;

public class UpdateProfessionalCommandHandler(DatabaseContext dbContext)
    : IHandler<ProfessionalDto, UpdateProfessionalCommand>
{
    public async Task<ProfessionalDto> HandleAsync(UpdateProfessionalCommand command,
        CancellationToken cancellationToken)
    {
        var professional = await dbContext.Professionals.FirstOrDefaultAsync(x=> x.Id == command.Id, cancellationToken);
        
        if (professional == null)
            throw new KeyNotFoundException($"Professional with Id: '{command.Id}' not found.");

        await dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            professional.UpdateEmail(command.Email);
            professional.UpdateFullName(command.FullName);
            professional.UpdateType(command.Type);
            
            dbContext.Professionals.Update(professional);
            await dbContext.SaveChangesAsync(cancellationToken);
            await dbContext.Database.CommitTransactionAsync(cancellationToken);
            
            return new ProfessionalDto(professional);
        }
        catch (Exception)
        {
            await dbContext.Database.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}