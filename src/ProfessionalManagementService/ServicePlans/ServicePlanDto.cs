using ProfessionalManagementService.ServicePlans.Common.Enums;

namespace ProfessionalManagementService.ServicePlans;

public class ServicePlanDto(ServicePlan servicePlan)
{
    /// <summary>
    /// Id do plano de serviço
    /// </summary>
    public Guid Id { get; private set; } = servicePlan.Id;
    
    /// <summary>
    /// Id do profissional associado ao plano de serviço
    /// </summary>
    public string ProfessionalId { get; private set; } = servicePlan.ProfessionalId;
    
    /// <summary>
    /// Título do plano de serviço
    /// </summary>
    public string Title { get; private set; } = servicePlan.Title;
    
    /// <summary>
    /// Descrição do plano de serviço
    /// </summary>
    public string Description { get; private set; } = servicePlan.Description;
    
    /// <summary>
    /// Duração do plano de serviço em dias
    /// </summary>
    public int DurationInDays { get; private set; } = servicePlan.DurationInDays;
    
    /// <summary>
    /// Preço do plano de serviço
    /// </summary>
    public double Price { get; private set; } = servicePlan.Price;
    
    /// <summary>
    /// Tipo do plano de serviço
    /// </summary>
    public EServicePlanType Type { get; private set; } = servicePlan.Type;
}