namespace ProfessionalManagementService.Reviews.CreateReview;

public class CreateReviewCommand(int rating, string? comment)
{
    public string ClientId { get; private set; }
    
    public string ProfessionalId { get; private set; }
    
    public Guid ServicePlanId { get; private set; }
    
    public int Rating { get; private set; } = rating;
    
    public string? Comment { get; private set; } = comment;
    
    public void SetClientId(string clientId)
    {
        ClientId = clientId;
    }
    
    public void SetProfessionalId(string professionalId)
    {
        ProfessionalId = professionalId;
    }
    
    public void SetServicePlanId(Guid servicePlanId)
    {
        ServicePlanId = servicePlanId;
    }
    
    public ClientProfessionalReview ToClientProfessionalReview()
    {
        return new ClientProfessionalReview(Rating, Comment);
    }
}