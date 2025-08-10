namespace ProfessionalManagementService.ServicePlans.DeleteServicePlanFromClient;

public class DeleteServicePlanFromClientCommand(string clientId, Guid servicePlanId, string loggedInUserId)
{
    /// <summary>
    /// Id do cliente ao qual o plano de serviço será removido
    /// </summary>
    public string ClientId { get; private set; } = clientId;
    
    /// <summary>
    /// Id do plano de serviço a ser removido do cliente
    /// </summary>
    public Guid ServicePlanId { get; private set; } = servicePlanId;

    /// <summary>
    /// Id do usuário logado que está realizando a operação
    /// </summary>
    public string LoggedInUserId { get; set; } = loggedInUserId;
}