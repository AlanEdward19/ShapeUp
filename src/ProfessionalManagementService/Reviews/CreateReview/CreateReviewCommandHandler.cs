using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Connections.Database;
using SharedKernel.Exceptions;

namespace ProfessionalManagementService.Reviews.CreateReview;

public class CreateReviewCommandHandler(DatabaseContext dbContext)
    : IHandler<ClientProfessionalReviewDto, CreateReviewCommand>
{
    public async Task<ClientProfessionalReviewDto> HandleAsync(CreateReviewCommand command,
        CancellationToken cancellationToken)
    {
        var client = await dbContext.Clients.FirstOrDefaultAsync(x => x.Id == command.ClientId, cancellationToken);
        if (client is null)
            throw new NotFoundException($"Client with ID '{command.ClientId}' does not exist.");

        var professional =
            await dbContext.Professionals.FirstOrDefaultAsync(x => x.Id == command.ProfessionalId, cancellationToken);
        if (professional is null)
            throw new ArgumentException($"Professional with ID '{command.ProfessionalId}' does not exist.");

        var servicePlan =
            await dbContext.ClientServicePlans.FirstOrDefaultAsync(x => x.ServicePlanId == command.ServicePlanId && 
                                                                      x.ClientId == command.ClientId,
                cancellationToken);
        if (servicePlan is null)
            throw new ArgumentException($"Service Plan with ID '{command.ServicePlanId}' does not exist.");

        var review = command.ToClientProfessionalReview();
        review.SetClient(client);
        review.SetProfessional(professional);
        review.SetClientServicePlan(servicePlan);
        
        var strategy = dbContext.Database.CreateExecutionStrategy();
        
        return await strategy.ExecuteAsync(async () =>
        {
            await dbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                await dbContext.ClientProfessionalReviews.AddAsync(review, cancellationToken);
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