namespace ProfessionalManagementService.ServicePlans.DeleteServicePlan;

/// <summary>
/// Comando para deletar um plano de serviço
/// </summary>
/// <param name="id"></param>
public class DeleteServicePlanCommand(Guid id)
{
    /// <summary>
    /// Id do plano de serviço a ser deletado
    /// </summary>
    public Guid Id { get; private set; } = id;
}