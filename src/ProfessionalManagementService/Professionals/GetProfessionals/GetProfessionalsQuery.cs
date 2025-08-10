namespace ProfessionalManagementService.Professionals.GetProfessionals;

public class GetProfessionalsQuery(string loggedInUserId)
{
    /// <summary>
    /// Id do usuário logado
    /// </summary>
    public string LoggedInUserId { get; set; } = loggedInUserId;
}