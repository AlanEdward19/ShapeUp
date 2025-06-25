using System.ComponentModel.DataAnnotations;
using ProfessionalManagementService.Common.Enums;
using ProfessionalManagementService.ServicePlans;

namespace ProfessionalManagementService.Professionals;

/// <summary>
/// Profissional que oferece serviços de nutrição e/ou treinamento
/// </summary>
public class Professional
{
    /// <summary>
    /// Id do profissional
    /// </summary>
    [Key]
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
    public EProfessionalType Type { get; private set; }
    
    /// <summary>
    /// Se o profissional está verificado
    /// </summary>
    public bool IsVerified { get; private set; }
    
    /// <summary>
    /// Data de criação do profissional
    /// </summary>
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    
    /// <summary>
    /// Data de atualização do profissional
    /// </summary>
    public DateTime UpdatedAt { get; private set; } = DateTime.Now;
    
    public virtual ICollection<ServicePlan> ServicePlans { get; set; } = new List<ServicePlan>();
    
    /// <summary>
    /// Método para adicionar um plano de serviço ao profissional.
    /// </summary>
    /// <param name="servicePlan"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void AddServicePlan(ServicePlan servicePlan)
    {
        if (servicePlan == null)
            throw new ArgumentNullException(nameof(servicePlan), "Service plan cannot be null.");
        
        servicePlan.SetProfessional(this);
        ServicePlans.Add(servicePlan);
    }
}