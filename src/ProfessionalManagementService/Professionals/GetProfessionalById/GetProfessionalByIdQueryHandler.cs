using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Connections.Database;
using SharedKernel.Exceptions;

namespace ProfessionalManagementService.Professionals.GetProfessionalById;

public class GetProfessionalByIdQueryHandler(DatabaseContext dbContext) : IHandler<ProfessionalDto, GetProfessionalByIdQuery>
{
    public async Task<ProfessionalDto> HandleAsync(GetProfessionalByIdQuery query, CancellationToken cancellationToken)
    {
        var professional = await dbContext.Professionals
            .Include(x => x.ServicePlans)
            .AsNoTracking()
            .FirstOrDefaultAsync(x=> x.Id == query.Id, cancellationToken);
        
        if (professional == null)
            throw new NotFoundException($"Professional with Id: '{query.Id}' not found.");
        
        return new ProfessionalDto(professional);
    }
}