using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Connections.Database;
using SharedKernel.Exceptions;

namespace ProfessionalManagementService.ServicePlans.GetServicePlanById;

public class GetServicePlanByIdQueryHandler(DatabaseContext dbContext) : IHandler<ServicePlanDto, GetServicePlanByIdQuery>
{
    public async Task<ServicePlanDto> HandleAsync(GetServicePlanByIdQuery query, CancellationToken cancellationToken)
    {
        var servicePlan = await dbContext.ServicePlans
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

        if (servicePlan == null)
            throw new NotFoundException($"Service plan with Id: '{query.Id}' not found.");

        return new ServicePlanDto(servicePlan);
    }
}