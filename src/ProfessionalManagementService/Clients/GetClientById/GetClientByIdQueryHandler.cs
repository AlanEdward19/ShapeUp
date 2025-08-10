using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Common.Enums;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Connections.Database;
using SharedKernel.Exceptions;

namespace ProfessionalManagementService.Clients.GetClientById;

public class GetClientByIdQueryHandler(DatabaseContext dbContext) : IHandler<ClientDto?, GetClientByIdQuery>
{
    public async Task<ClientDto?> HandleAsync(GetClientByIdQuery query, CancellationToken cancellationToken)
    {
        var client = await dbContext.Clients
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.ClientServicePlans)
            .ThenInclude(x => x.ServicePlan)
            .Include(x => x.ClientProfessionalReviews)
            .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken: cancellationToken);

        if (client == null)
            return null;

        var professional = await dbContext.Professionals
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken: cancellationToken);

        if (professional == null) return new ClientDto(client);

        var (isNutritionist, isTrainer) = professional.Type switch
        {
            EProfessionalType.Nutritionist => (true, false),
            EProfessionalType.Trainer => (false, true),
            EProfessionalType.Both => (true, true),
            _ => (false, false)
        };

        return new ClientDto(client, isNutritionist, isTrainer);
    }
}