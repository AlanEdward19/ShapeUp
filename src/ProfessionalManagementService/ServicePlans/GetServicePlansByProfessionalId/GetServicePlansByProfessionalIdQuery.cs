using ProfessionalManagementService.ServicePlans.Common.Enums;

namespace ProfessionalManagementService.ServicePlans.GetServicePlansByProfessionalId;

/// <summary>
/// Query para obter todos os planos de serviços de um profissional específico
/// </summary>
public class GetServicePlansByProfessionalIdQuery(string professionalId, EServicePlanType? type)
{
    /// <summary>
    /// Id do profissional para o qual se deseja obter os planos de serviços
    /// </summary>
    public string ProfessionalId { get; private set; } = professionalId;
    
    /// <summary>
    /// Tipo de plano de serviço a ser filtrado
    /// </summary>
    public EServicePlanType? Type { get; private set; }
}