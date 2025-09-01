using ProfessionalManagementService.ServicePlans.Common.Enums;

namespace ProfessionalManagementService.ServicePlans.CreateServicePlan;

/// <summary>
/// Comando para criar um novo plano de serviço
/// </summary>
public class CreateServicePlanCommand
{
    /// <summary>
    /// Id do profissional
    /// </summary>
    private string ProfessionalId { get; set; }
    
    /// <summary>
    /// Titulo do plano de serviço
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Descrição do plano de serviço
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Duração do plano de serviço em dias
    /// </summary>
    public int DurationInDays { get; set; }
    
    /// <summary>
    /// Preço do plano de serviço
    /// </summary>
    public double Price { get; set; }
    
    /// <summary>
    /// Tipo do plano de serviço
    /// </summary>
    public EServicePlanType Type { get; set; }
    
    /// <summary>
    /// Método para definir o Id do profissional
    /// </summary>
    /// <param name="professionalId"></param>
    public void SetProfessionalId(string professionalId)
    {
        ProfessionalId = professionalId;
    }
    
    /// <summary>
    /// Método para ler o Id do profissional
    /// </summary>
    /// <returns></returns>
    public string GetProfessionalId() => ProfessionalId;
    
    /// <summary>
    /// Método para converter o comando em um objeto ServicePlan
    /// </summary>
    /// <returns></returns>
    public ServicePlan ToServicePlan()
    {
        return new ServicePlan(ProfessionalId, Title, Description, DurationInDays, Price, Type);
    }
}