using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Connections.Database;
using SharedKernel.Exceptions;

namespace ProfessionalManagementService.Professionals.UpdateProfessional;

public class UpdateProfessionalCommandHandler(DatabaseContext dbContext)
    : IHandler<ProfessionalDto, UpdateProfessionalCommand>
{
    public async Task<ProfessionalDto> HandleAsync(UpdateProfessionalCommand command,
        CancellationToken cancellationToken)
    {
        var professional = await dbContext.Professionals.FirstOrDefaultAsync(x=> x.Id == command.GetId(), cancellationToken);
        
        if (professional == null)
            throw new NotFoundException($"Professional with Id: '{command.GetId()}' not found.");

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