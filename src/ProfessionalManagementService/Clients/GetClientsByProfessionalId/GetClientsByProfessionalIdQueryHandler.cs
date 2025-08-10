using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Connections.Database;
using SharedKernel.Exceptions;

namespace ProfessionalManagementService.Clients.GetClientsByProfessionalId;

public class GetClientsByProfessionalIdQueryHandler(DatabaseContext dbContext) : IHandler<List<ClientDto>, GetClientsByProfessionalIdQuery>
{
    public async Task<List<ClientDto>> HandleAsync(GetClientsByProfessionalIdQuery query, CancellationToken cancellationToken)
    {
        var professionalExists = await dbContext.Professionals
            .AsNoTracking()
            .AnyAsync(x => x.Id == query.ProfessionalId, cancellationToken);
        
        if (!professionalExists)
            throw new NotFoundException($"Professional with Id: '{query.ProfessionalId}' not found.");
        
        var clients = await dbContext.Clients
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.ClientServicePlans)
            .ThenInclude(x => x.ServicePlan)
            .Include(x => x.ClientProfessionalReviews)
            .Where(x => x.ClientServicePlans.Any(y => y.ServicePlan.ProfessionalId == query.ProfessionalId))
            .Select(x => new ClientDto(x, false, false))
            .ToListAsync(cancellationToken);

        return clients;
    }
}