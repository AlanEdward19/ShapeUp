namespace ProfessionalManagementService.Reviews.DeleteReview;

public class DeleteReviewCommand(string clientId, string professionalId, Guid servicePlanId)
{
    public string ClientId { get; private set; } = clientId;
    
    public string ProfessionalId { get; private set; } = professionalId;
    
    public Guid ServicePlanId { get; private set; } = servicePlanId;
}