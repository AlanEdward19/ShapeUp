using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Connections.Database;
using SharedKernel.Exceptions;

namespace ProfessionalManagementService.Reviews.DeleteReview;

public class DeleteReviewCommandHandler(DatabaseContext dbContext) : IHandler<bool, DeleteReviewCommand>
{
    public async Task<bool> HandleAsync(DeleteReviewCommand command, CancellationToken cancellationToken)
    {
        var client = await dbContext.Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == command.ClientId, cancellationToken);
        if (client is null)
            throw new NotFoundException($"Client with ID '{command.ClientId}' does not exist.");

        var professional =
            await dbContext.Professionals
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == command.ProfessionalId, cancellationToken);
        if (professional is null)
            throw new ArgumentException($"Professional with ID '{command.ProfessionalId}' does not exist.");

        var servicePlan =
            await dbContext.ClientServicePlans
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ServicePlanId == command.ServicePlanId &&
                                          x.ClientId == command.ClientId,
                    cancellationToken);
        if (servicePlan is null)
            throw new ArgumentException($"Service Plan with ID '{command.ServicePlanId}' does not exist.");

        var review =
            await dbContext.ClientProfessionalReviews.FirstOrDefaultAsync(
                x => x.ClientId == client.Id && x.ProfessionalId == professional.Id &&
                     x.ClientServicePlanId == servicePlan.Id, cancellationToken);
        if (review is null)
            throw new NotFoundException($"Review for Client ID '{client.Id}', Professional ID '{professional.Id}' " +
                                        $"and Service Plan ID '{servicePlan.Id}' does not exist.");
        
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