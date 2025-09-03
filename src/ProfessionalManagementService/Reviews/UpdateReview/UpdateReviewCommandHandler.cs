using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Connections.Database;
using SharedKernel.Exceptions;

namespace ProfessionalManagementService.Reviews.UpdateReview;

public class UpdateReviewCommandHandler(DatabaseContext dbContext)
    : IHandler<ClientProfessionalReviewDto, UpdateReviewCommand>
{
    public async Task<ClientProfessionalReviewDto> HandleAsync(UpdateReviewCommand command,
        CancellationToken cancellationToken)
    {
        var review = await dbContext.ClientProfessionalReviews
            .Include(x => x.Client)
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);
        if (review == null)
            throw new NotFoundException($"Review with ID '{command.Id}' does not exist.");

        var strategy = dbContext.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                review.UpdateRating(command.Rating);
                review.UpdateComment(command.Comment);

                dbContext.ClientProfessionalReviews.Update(review);
                await dbContext.SaveChangesAsync(cancellationToken);
                await dbContext.Database.CommitTransactionAsync(cancellationToken);

                return new ClientProfessionalReviewDto(review);
            }
            catch (Exception)
            {
                await dbContext.Database.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        });
    }
}