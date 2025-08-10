using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Connections.Database;
using SharedKernel.Exceptions;

namespace ProfessionalManagementService.Scores.GetProfessionalScoreByProfessionalId;

public class GetProfessionalScoreByProfessionalIdQueryHandler(DatabaseContext dbContext)
    : IHandler<ProfessionalScore, GetProfessionalScoreByProfessionalIdQuery>
{
    public async Task<ProfessionalScore> HandleAsync(GetProfessionalScoreByProfessionalIdQuery query,
        CancellationToken cancellationToken)
    {
        var reviews =
            await dbContext.ClientProfessionalReviews
                .AsNoTracking()
                .Where(x => x.ProfessionalId == query.ProfessionalId)
                .ToListAsync(cancellationToken);

        if (!reviews.Any())
            return new ProfessionalScore(query.ProfessionalId, 0, 0, DateTime.Now);

        ProfessionalScore professionalScore = new(
            query.ProfessionalId,
            reviews.Select(x => x.Rating).Average(),
            reviews.Count,
            DateTime.Now
        );

        return professionalScore;
    }
}