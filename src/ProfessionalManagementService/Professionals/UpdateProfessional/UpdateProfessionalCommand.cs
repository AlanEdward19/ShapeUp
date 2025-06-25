using ProfessionalManagementService.Common.Enums;

namespace ProfessionalManagementService.Professionals.UpdateProfessional;

/// <summary>
/// Comando para atualizar um profissional
/// </summary>
public class UpdateProfessionalCommand
{
    /// <summary>
    /// Id do profissional
    /// </summary>
    public string Id { get; private set; }
    
    /// <summary>
    /// Nome completo do profissional
    /// </summary>
    public string? FullName { get; set; }
    
    /// <summary>
    /// Email do profissional
    /// </summary>
    public string? Email { get; set; }
    
    /// <summary>
    /// Tipo de profissional
    /// </summary>
    public EProfessionalType? Type { get; set; }
    
    /// <summary>
    /// Método para definir o id do profissional
    /// </summary>
    /// <param name="id"></param>
    public void SetId(string id)
    {
        Id = id;
    }
}