using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProfessionalManagementService.Professionals;

namespace ProfessionalManagementService.ServicePlans;

/// <summary>
/// Plano de serviço oferecido por um profissional
/// </summary>
public class ServicePlan
{
    /// <summary>
    /// Id do plano de serviço
    /// </summary>
    [Key]
    public Guid Id { get; private set; }
    
    /// <summary>
    /// Id do profissional associado ao plano de serviço
    /// </summary>
    public string ProfessionalId { get; private set; }
    
    /// <summary>
    /// Título do plano de serviço
    /// </summary>
    public string Title { get; private set; }
    
    /// <summary>
    /// Descrição do plano de serviço
    /// </summary>
    public string Description { get; private set; }
    
    /// <summary>
    /// Duração do plano de serviço em dias
    /// </summary>
    public int DurationInDays { get; private set; }
    
    /// <summary>
    /// Preço do plano de serviço
    /// </summary>
    public double Price { get; private set; }
    
    /// <summary>
    /// Data de criação do plano de serviço
    /// </summary>
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    
    /// <summary>
    /// Data de atualização do plano de serviço
    /// </summary>
    public DateTime UpdatedAt { get; private set; } = DateTime.Now;
    
    /// <summary>
    /// Profissional associado ao plano de serviço
    /// </summary>
    [ForeignKey(nameof(ProfessionalId))]
    public virtual Professional Professional { get; private set; }
    
    /// <summary>
    /// Método para definir o profissional associado ao plano de serviço.
    /// </summary>
    /// <param name="professional"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void SetProfessional(Professional professional) 
    {
        Professional = professional ?? throw new ArgumentNullException(nameof(professional), "Professional cannot be null.");
        ProfessionalId = professional.Id;
    }
}