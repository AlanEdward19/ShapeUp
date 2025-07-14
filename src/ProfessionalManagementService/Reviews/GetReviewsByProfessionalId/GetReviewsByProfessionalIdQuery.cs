namespace ProfessionalManagementService.Reviews.GetReviewsByProfessionalId;

public class GetReviewsByProfessionalIdQuery(string professionalId)
{
    public string ProfessionalId { get; private set; } = professionalId;
}