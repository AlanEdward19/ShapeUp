namespace ProfessionalManagementService.Professionals.GetProfessionalById;

/// <summary>
/// Query para buscar um profissional por Id
/// </summary>
/// <param name="id"></param>
public class GetProfessionalByIdQuery(string id)
{
    /// <summary>
    /// Id do profissional a ser buscado
    /// </summary>
    public string Id { get; private set; } = id;
}