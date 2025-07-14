using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Connections.Database;
using SharedKernel.Exceptions;

namespace ProfessionalManagementService.Clients.GetClientById;

public class GetClientByIdQueryHandler(DatabaseContext dbContext) : IHandler<ClientDto, GetClientByIdQuery>
{
    public async Task<ClientDto> HandleAsync(GetClientByIdQuery query, CancellationToken cancellationToken)
    {
        var client = await dbContext.Clients
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.ClientServicePlans)
            .ThenInclude(x => x.ServicePlan)
            .Include(x => x.ClientProfessionalReviews)
            .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken: cancellationToken);

        if (client == null)
            throw new NotFoundException($"Client with Id: '{query.Id}' not found.");
        
        return new ClientDto(client);
    }
}