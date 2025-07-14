namespace ProfessionalManagementService.Scores.GetProfessionalScoreByProfessionalId;

public class GetProfessionalScoreByProfessionalIdQuery(string professionalId)
{
    public string ProfessionalId { get; private set; } = professionalId;
}