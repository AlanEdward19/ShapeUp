namespace ProfessionalManagementService.Reviews.DeleteReview;

public class DeleteReviewCommand(Guid id)
{
    public Guid Id { get; private set; } = id;
}