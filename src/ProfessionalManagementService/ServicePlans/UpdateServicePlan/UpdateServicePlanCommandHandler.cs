using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Connections.Database;
using SharedKernel.Exceptions;

namespace ProfessionalManagementService.ServicePlans.UpdateServicePlan;

public class UpdateServicePlanCommandHandler(DatabaseContext dbContext)
    : IHandler<ServicePlanDto, UpdateServicePlanCommand>
{
    public async Task<ServicePlanDto> HandleAsync(UpdateServicePlanCommand command, CancellationToken cancellationToken)
    {
        var servicePlan = await
            dbContext.ServicePlans.FirstOrDefaultAsync(x => x.Id == command.GetId(), cancellationToken);
        
        if (servicePlan == null)
            throw new NotFoundException($"Service plan with Id: '{command.GetId()}' not found.");
        
        await dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            servicePlan.UpdateTitle(command.Title);
            servicePlan.UpdateDescription(command.Description);
            servicePlan.UpdateDurationInDays(command.DurationInDays);
            servicePlan.UpdatePrice(command.Price);
            
            dbContext.ServicePlans.Update(servicePlan);
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