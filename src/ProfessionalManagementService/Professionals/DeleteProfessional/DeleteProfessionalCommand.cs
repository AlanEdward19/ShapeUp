namespace ProfessionalManagementService.Professionals.DeleteProfessional;

/// <summary>
/// Comando para deletar um profissional
/// </summary>
/// <param name="id"></param>
public class DeleteProfessionalCommand(string id)
{
    /// <summary>
    /// Id do profissional a ser deletado
    /// </summary>
    public string Id { get; private set; } = id;
}