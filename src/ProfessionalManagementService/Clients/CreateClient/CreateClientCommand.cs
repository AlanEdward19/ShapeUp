namespace ProfessionalManagementService.Clients.CreateClient;

/// <summary>
/// Comando para criar um cliente
/// </summary>
/// <param name="id"></param>
/// <param name="email"></param>
public class CreateClientCommand(string id, string email)
{
    /// <summary>
    /// Id do cliente
    /// </summary>
    public string Id { get; private set; } = id;
    
    /// <summary>
    /// Email do cliente
    /// </summary>
    public string Email { get; private set; } = email;
    
    /// <summary>
    /// Método para converter o comando em um objeto Client
    /// </summary>
    /// <returns></returns>
    public Client ToClient()
    {
        return new Client(Id, Email);
    }
}