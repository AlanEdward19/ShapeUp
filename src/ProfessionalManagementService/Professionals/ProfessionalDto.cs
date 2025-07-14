using ProfessionalManagementService.Common.Enums;
using ProfessionalManagementService.ServicePlans;

namespace ProfessionalManagementService.Professionals;

public class ProfessionalDto(Professional professional)
{
    /// <summary>
    /// Id do profissional
    /// </summary>
    public string Id { get; private set; } = professional.Id;

    /// <summary>
    /// Email do profissional
    /// </summary>
    public string Email { get; private set; } = professional.Email;

    /// <summary>
    /// Tipo de profissional
    /// </summary>
    public EProfessionalType Type { get; private set; } = professional.Type;

    /// <summary>
    /// Se o profissional está verificado
    /// </summary>
    public bool IsVerified { get; private set; }
    
    public ICollection<ServicePlanDto> ServicePlans { get; init; } = professional.ServicePlans
        .Select(sp => new ServicePlanDto(sp))
        .ToList();
}