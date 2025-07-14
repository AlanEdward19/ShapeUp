namespace ProfessionalManagementService.Reviews;

public class ClientProfessionalReviewDto(ClientProfessionalReview clientProfessionalReview)
{
    public Guid Id { get; private set; } = clientProfessionalReview.Id;
    
    public string ClientId { get; private set; } = clientProfessionalReview.ClientId;
    
    public string? ProfessionalId { get; private set; } = clientProfessionalReview.ProfessionalId;
    
    public Guid ClientServicePlanId { get; private set; } = clientProfessionalReview.ClientServicePlanId;
    
    public int Rating { get; private set; } = clientProfessionalReview.Rating;
    
    public string? Comment { get; private set; } = clientProfessionalReview.Comment;
    
    public DateTime CreatedAt { get; private set; } = clientProfessionalReview.CreatedAt;
    
    public DateTime UpdatedAt { get; private set; } = clientProfessionalReview.UpdatedAt;
}