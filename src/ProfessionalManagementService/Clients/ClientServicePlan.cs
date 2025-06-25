using System.ComponentModel.DataAnnotations.Schema;
using ProfessionalManagementService.Common.Enums;
using ProfessionalManagementService.ServicePlans;

namespace ProfessionalManagementService.Clients;

/// <summary>
/// Plano de serviço associado a um cliente
/// </summary>
public class ClientServicePlan
{
    /// <summary>
    /// Id do plano de serviço do cliente
    /// </summary>
    public Guid Id { get; private set; }
    
    /// <summary>
    /// Id do cliente associado ao plano de serviço
    /// </summary>
    public string ClientId { get; private set; }
    
    /// <summary>
    /// Id do plano de serviço associado ao cliente
    /// </summary>
    public Guid ServicePlanId { get; private set; }
    
    /// <summary>
    /// Data de início do plano de serviço do cliente
    /// </summary>
    public DateTime StartDate { get; private set; }
    
    /// <summary>
    /// Data de término do plano de serviço do cliente
    /// </summary>
    public DateTime EndDate { get; private set; }
    
    /// <summary>
    /// Status da assinatura do plano de serviço do cliente
    /// </summary>
    public ESubscriptionStatus Status { get; private set; }
    
    /// <summary>
    /// Feedback do cliente sobre o plano de serviço
    /// </summary>
    public string? Feedback { get; private set; }
    
    /// <summary>
    /// Data de criação do plano de serviço do cliente
    /// </summary>
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    
    /// <summary>
    /// Data de atualização do plano de serviço do cliente
    /// </summary>
    public DateTime UpdatedAt { get; private set; } = DateTime.Now;
    
    /// <summary>
    /// Cliente associado ao plano de serviço
    /// </summary>
    [ForeignKey(nameof(ClientId))]
    public virtual Client Client { get; private set; }
    
    /// <summary>
    /// Plano de serviço associado ao cliente
    /// </summary>
    [ForeignKey(nameof(ServicePlanId))]
    public virtual ServicePlan ServicePlan { get; private set; }
    
    /// <summary>
    /// Método para definir o cliente associado ao plano de serviço.
    /// </summary>
    /// <param name="client"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void SetClient(Client client)
    {
        Client = client ?? throw new ArgumentNullException(nameof(client), "Client cannot be null.");
        ClientId = client.Id;
    }
}