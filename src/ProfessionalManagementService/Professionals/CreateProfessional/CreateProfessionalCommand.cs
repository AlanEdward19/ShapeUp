using ProfessionalManagementService.Common.Enums;

namespace ProfessionalManagementService.Professionals.CreateProfessional;

/// <summary>
/// Comando para criar um profissional
/// </summary>
public class CreateProfessionalCommand
{
    /// <summary>
    /// Id do profissional
    /// </summary>
    private string Id { get; set; }
    
    /// <summary>
    /// Email do profissional
    /// </summary>
    private string Email { get; set; }
    
    /// <summary>
    /// Nome do profissional
    /// </summary>
    private string Name { get; set; }
    
    /// <summary>
    /// Tipo de profissional
    /// </summary>
    public EProfessionalType Type { get; set; }

    /// <summary>
    /// Método para definir o id do profissional
    /// </summary>
    /// <param name="id"></param>
    public void SetId(string id)
    {
        Id = id;
    }
    
    /// <summary>
    /// Método para obter o id do profissional
    /// </summary>
    /// <returns></returns>
    public string GetId() => Id;
    
    /// <summary>
    /// Método para definir o email do profissional
    /// </summary>
    /// <param name="email"></param>
    public void SetEmail(string email)
    {
        Email = email;
    }
    
    /// <summary>
    /// Método para definir o nome do profissional
    /// </summary>
    /// <param name="name"></param>
    public void SetName(string name)
    {
        Name = name;
    }
    
    /// <summary>
    /// Método para converter o comando em um objeto Professional
    /// </summary>
    /// <returns></returns>
    public Professional ToProfessional()
    {
        return new Professional(Id, Email.ToLower(), Name, Type, false);
    }
}