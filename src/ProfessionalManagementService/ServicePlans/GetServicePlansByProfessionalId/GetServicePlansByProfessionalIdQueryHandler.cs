using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Connections.Database;
using SharedKernel.Exceptions;

namespace ProfessionalManagementService.ServicePlans.GetServicePlansByProfessionalId;

public class GetServicePlansByProfessionalIdQueryHandler(DatabaseContext dbContext) : IHandler<List<ServicePlanDto>, GetServicePlansByProfessionalIdQuery>
{
    public async Task<List<ServicePlanDto>> HandleAsync(GetServicePlansByProfessionalIdQuery query, CancellationToken cancellationToken)
    {
        var professionalExists = await dbContext.Professionals
            .AsNoTracking()
            .AnyAsync(x => x.Id == query.ProfessionalId, cancellationToken);
        
        if (!professionalExists)
            throw new NotFoundException($"Professional with Id: '{query.ProfessionalId}' not found.");
        
        IQueryable<ServicePlan> servicePlanQuery = dbContext.ServicePlans
            .AsNoTracking()
            .Where(x => x.ProfessionalId == query.ProfessionalId);
        
        if(query.Type != null)
            servicePlanQuery = servicePlanQuery.Where(x => x.Type == query.Type);
        
        var servicePlans = await servicePlanQuery
            .ToListAsync(cancellationToken);

        return servicePlans
            .Select(x => new ServicePlanDto(x))
            .ToList();
    }
}