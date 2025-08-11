using ProfessionalManagementService.Common.Enums;

namespace ProfessionalManagementService.ServicePlans.ChangeServicePlanStatusFromClient;

/// <summary>
/// Comando para alterar o status de um plano de serviço associado a um cliente
/// </summary>
/// <param name="clientId"></param>
/// <param name="servicePlanId"></param>
/// <param name="loggedInUserId"></param>
/// <param name="subscriptionStatus"></param>
public class ChangeServicePlanStatusFromClientCommand(string clientId, Guid servicePlanId, string loggedInUserId, ESubscriptionStatus subscriptionStatus, string reason)
{
    /// <summary>
    /// Id do cliente ao qual o plano de serviço será adicionado
    /// </summary>
    public string ClientId { get; private set; } = clientId;
    
    /// <summary>
    /// Id do plano de serviço a ser adicionado ao cliente
    /// </summary>
    public Guid ServicePlanId { get; private set; } = servicePlanId;

    /// <summary>
    /// Id do usuário logado que está realizando a operação
    /// </summary>
    public string LoggedInUserId { get; private set; } = loggedInUserId;
    
    /// <summary>
    /// Status da assinatura do plano de serviço
    /// </summary>
    public ESubscriptionStatus SubscriptionStatus { get; private set; } = subscriptionStatus;
    
    /// <summary>
    /// Razão para a alteração do status do plano de serviço
    /// </summary>
    public string Reason { get; private set; } = reason;
}