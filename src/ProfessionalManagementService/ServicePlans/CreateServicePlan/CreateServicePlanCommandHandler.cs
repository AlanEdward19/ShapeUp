using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Connections.Database;
using SharedKernel.Exceptions;

namespace ProfessionalManagementService.ServicePlans.CreateServicePlan;

public class CreateServicePlanCommandHandler(DatabaseContext dbContext)
    : IHandler<ServicePlanDto, CreateServicePlanCommand>
{
    public async Task<ServicePlanDto> HandleAsync(CreateServicePlanCommand command, CancellationToken cancellationToken)
    {
        bool isProfessional = await dbContext.Professionals
            .AnyAsync(p => p.Id == command.GetProfessionalId(), cancellationToken);

        if (!isProfessional)
            throw new ForbiddenException("You do not have permission to create a service plan.");

        var servicePlan = command.ToServicePlan();

        var strategy = dbContext.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
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
        });
    }
}