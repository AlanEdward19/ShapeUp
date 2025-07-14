namespace ProfessionalManagementService.Clients.GetClientsByProfessionalId;

/// <summary>
/// Query para obter os clientes de um profissional pelo seu ID
/// </summary>
/// <param name="professionalId"></param>
public class GetClientsByProfessionalIdQuery(string professionalId)
{
    /// <summary>
    /// Id do profissional para o qual se deseja obter os clientes
    /// </summary>
    public string ProfessionalId { get; private set; } = professionalId;
}