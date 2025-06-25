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
    public string Id { get; private set; }
    
    /// <summary>
    /// Nome completo do profissional
    /// </summary>
    public string FullName { get; private set; }
    
    /// <summary>
    /// Email do profissional
    /// </summary>
    public string Email { get; private set; }
    
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
    /// Método para definir o nome completo do profissional
    /// </summary>
    /// <param name="fullName"></param>
    public void SetFullName(string fullName)
    {
        FullName = fullName;
    }
    
    /// <summary>
    /// Método para definir o email do profissional
    /// </summary>
    /// <param name="email"></param>
    public void SetEmail(string email)
    {
        Email = email;
    }
    
    /// <summary>
    /// Método para converter o comando em um objeto Professional
    /// </summary>
    /// <returns></returns>
    public Professional ToProfessional()
    {
        return new Professional(Id, FullName, Email, Type, false);
    }
}