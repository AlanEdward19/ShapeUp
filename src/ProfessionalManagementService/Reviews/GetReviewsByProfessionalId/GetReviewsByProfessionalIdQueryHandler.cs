using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Connections.Database;
using SharedKernel.Exceptions;

namespace ProfessionalManagementService.Reviews.GetReviewsByProfessionalId;

public class GetReviewsByProfessionalIdQueryHandler(DatabaseContext dbContext) : IHandler<List<ClientProfessionalReviewDto>, GetReviewsByProfessionalIdQuery>
{
    public async Task<List<ClientProfessionalReviewDto>> HandleAsync(GetReviewsByProfessionalIdQuery command, CancellationToken cancellationToken)
    {
        bool professionalExists = await dbContext.Professionals
            .AnyAsync(x => x.Id == command.ProfessionalId, cancellationToken);
        
        if (!professionalExists)
            throw new NotFoundException($"Professional with ID '{command.ProfessionalId}' does not exist.");
        
        var reviews = await dbContext.ClientProfessionalReviews
            .Where(x => x.ProfessionalId == command.ProfessionalId)
            .Select(x => new ClientProfessionalReviewDto(x))
            .ToListAsync(cancellationToken);

        return reviews;
    }
}