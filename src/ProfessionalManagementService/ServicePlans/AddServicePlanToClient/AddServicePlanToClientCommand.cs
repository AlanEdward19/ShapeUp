namespace ProfessionalManagementService.ServicePlans.AddServicePlanToClient;

/// <summary>
/// Comando para adicionar um plano de serviço a um cliente
/// </summary>
/// <param name="clientId"></param>
/// <param name="servicePlanId"></param>
public class AddServicePlanToClientCommand(string clientId, Guid servicePlanId, string  loggedInUserId)
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
}