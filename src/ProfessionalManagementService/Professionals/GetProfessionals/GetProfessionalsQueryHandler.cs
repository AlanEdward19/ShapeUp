using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Connections.Database;
using ProfessionalManagementService.Professionals.GetProfessionalById;
using SharedKernel.Exceptions;

namespace ProfessionalManagementService.Professionals.GetProfessionals;

public class GetProfessionalsQueryHandler(DatabaseContext dbContext) : IHandler<List<ProfessionalDto>, GetProfessionalsQuery>
{
    public async Task<List<ProfessionalDto>> HandleAsync(GetProfessionalsQuery query, CancellationToken cancellationToken)
    {
        var professionals = await dbContext.Professionals
            .Include(x => x.ServicePlans)
            .AsNoTracking()
            .Select(x => new ProfessionalDto(x))
            .ToListAsync(cancellationToken);

        return professionals;
    }
}