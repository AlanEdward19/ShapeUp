namespace ProfessionalManagementService.ServicePlans.UpdateServicePlan;

/// <summary>
/// Comando para atualizar um plano de serviço
/// </summary>
public class UpdateServicePlanCommand
{
    /// <summary>
    /// Id do plano de serviço a ser atualizado
    /// </summary>
    private Guid Id { get; set; }
    
    /// <summary>
    /// Titulo do plano de serviço
    /// </summary>
    public string? Title { get; set; }
    
    /// <summary>
    /// Descrição do plano de serviço
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Duração do plano de serviço em dias
    /// </summary>
    public int? DurationInDays { get; set; }
    
    /// <summary>
    /// Preço do plano de serviço
    /// </summary>
    public double? Price { get; set; }
    
    /// <summary>
    /// Método para definir o id do plano de serviço
    /// </summary>
    /// <param name="id"></param>
    public void SetId(Guid id)
    {
        Id = id;
    }
    
    /// <summary>
    /// Método para ler o id do plano de serviço
    /// </summary>
    /// <returns></returns>
    public Guid GetId() => Id;
}