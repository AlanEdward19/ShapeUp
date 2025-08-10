namespace ProfessionalManagementService.Reviews;

public class ClientProfessionalReviewDto(ClientProfessionalReview clientProfessionalReview)
{
    public Guid Id { get; private set; } = clientProfessionalReview.Id;
    
    public string ClientId { get; private set; } = clientProfessionalReview.ClientId;
    
    public string ClientName { get; private set; } = clientProfessionalReview.Client.Name;
    
    public string? ProfessionalId { get; private set; } = clientProfessionalReview.ProfessionalId;
    
    public Guid ClientServicePlanId { get; private set; } = clientProfessionalReview.ClientServicePlanId;
    
    public int Rating { get; private set; } = clientProfessionalReview.Rating;
    
    public string? Comment { get; private set; } = clientProfessionalReview.Comment;
    
    public DateTime LastUpdatedAt { get; private set; } = clientProfessionalReview.UpdatedAt;
}