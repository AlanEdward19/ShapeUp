using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Connections.Database;

namespace ProfessionalManagementService.Professionals.CreateProfessional;

public class CreateProfessionalCommandHandler(DatabaseContext dbContext)
    : IHandler<ProfessionalDto, CreateProfessionalCommand>
{
    public async Task<ProfessionalDto> HandleAsync(CreateProfessionalCommand command,
        CancellationToken cancellationToken)
    {
        var professional = command.ToProfessional();

        await dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await dbContext.Professionals.AddAsync(professional, cancellationToken);
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