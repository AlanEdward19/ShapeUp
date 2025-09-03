using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Connections.Database;
using SharedKernel.Exceptions;

namespace ProfessionalManagementService.ServicePlans.DeleteServicePlan;

public class DeleteServicePlanCommandHandler(DatabaseContext dbContext) : IHandler<bool, DeleteServicePlanCommand>
{
    public async Task<bool> HandleAsync(DeleteServicePlanCommand command, CancellationToken cancellationToken)
    {
        var servicePlan = dbContext.ServicePlans.FirstOrDefault(x => x.Id == command.Id);

        if (servicePlan == null)
            throw new NotFoundException($"Service plan with Id: '{command.Id}' not found.");
        
        var strategy = dbContext.Database.CreateExecutionStrategy();
        
        return await strategy.ExecuteAsync(async () =>
        {
            await dbContext.Database.BeginTransactionAsync(cancellationToken);
            
            try
            {
                dbContext.ServicePlans.Remove(servicePlan);
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