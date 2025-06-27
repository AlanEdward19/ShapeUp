namespace ProfessionalManagementService.Clients.UpdateClient;

/// <summary>
/// Comando para atualizar um cliente
/// </summary>
public class UpdateClientCommand
{
    /// <summary>
    /// Id do cliente a ser atualizado
    /// </summary>
    private string Id { get; set; }

    /// <summary>
    /// Email do cliente
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Método para setar o Id do cliente
    /// </summary>
    /// <param name="id"></param>
    public void SetId(string id)
    {
        Id = id;
    }
    
    /// <summary>
    /// Método para obter o Id do cliente
    /// </summary>
    /// <returns></returns>
    public string GetId() => Id;
}