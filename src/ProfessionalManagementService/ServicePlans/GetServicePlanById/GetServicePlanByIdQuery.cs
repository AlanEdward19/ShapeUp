namespace ProfessionalManagementService.ServicePlans.GetServicePlanById;

/// <summary>
/// Query para obter um plano de serviço por ID
/// </summary>
/// <param name="id"></param>
public class GetServicePlanByIdQuery(Guid id)
{
    /// <summary>
    /// Id do plano de serviço a ser obtido
    /// </summary>
    public Guid Id { get; private set; } = id;
}