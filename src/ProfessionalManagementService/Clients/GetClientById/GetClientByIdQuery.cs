namespace ProfessionalManagementService.Clients.GetClientById;

/// <summary>
/// Query para obter um cliente pelo seu ID
/// </summary>
/// <param name="id"></param>
public class GetClientByIdQuery(string id)
{
    /// <summary>
    /// Id do cliente a ser obtido
    /// </summary>
    public string Id { get; private set; } = id;
}