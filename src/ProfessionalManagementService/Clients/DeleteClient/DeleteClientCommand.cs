namespace ProfessionalManagementService.Clients.DeleteClient;

/// <summary>
/// Comando para excluir um cliente
/// </summary>
/// <param name="id"></param>
public class DeleteClientCommand(string id)
{
    /// <summary>
    /// Id do cliente a ser excluído
    /// </summary>
    public string Id { get; private set; } = id;
}