using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Connections.Database;
using SharedKernel.Exceptions;

namespace ProfessionalManagementService.Reviews.DeleteReview;

public class DeleteReviewCommandHandler(DatabaseContext dbContext) : IHandler<bool, DeleteReviewCommand>
{
    public async Task<bool> HandleAsync(DeleteReviewCommand command, CancellationToken cancellationToken)
    {
        var review = await dbContext.ClientProfessionalReviews.FirstOrDefaultAsync(
            x => x.Id == command.Id, cancellationToken);
        if (review is null)
            throw new NotFoundException($"Review with ID '{command.Id}' does not exist.");

        await dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            dbContext.ClientProfessionalReviews.Remove(review);
            await dbContext.SaveChangesAsync(cancellationToken);
            await dbContext.Database.CommitTransactionAsync(cancellationToken);

            return true;
        }
        catch (Exception)
        {
            await dbContext.Database.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}