using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Connections.Database;

namespace ProfessionalManagementService.ServicePlans.CreateServicePlan;

public class CreateServicePlanCommandHandler(DatabaseContext dbContext) : IHandler<ServicePlanDto, CreateServicePlanCommand>
{
    public async Task<ServicePlanDto> HandleAsync(CreateServicePlanCommand command, CancellationToken cancellationToken)
    {
        var servicePlan = command.ToServicePlan();

        await dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await dbContext.ServicePlans.AddAsync(servicePlan, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            await dbContext.Database.CommitTransactionAsync(cancellationToken);

            return new ServicePlanDto(servicePlan);
        }
        catch (Exception)
        {
            await dbContext.Database.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}