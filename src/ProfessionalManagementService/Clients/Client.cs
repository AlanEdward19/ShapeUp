using System.ComponentModel.DataAnnotations;
using ProfessionalManagementService.Professionals;
using ProfessionalManagementService.Reviews;

namespace ProfessionalManagementService.Clients;

/// <summary>
/// Cliente do serviço de gerenciamento profissional
/// </summary>
public class Client
{
    [Key]
    public string Id { get; private set; }
    
    public string FullName { get; private set; }
    
    public string Email { get; private set; }
    
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    
    public DateTime UpdatedAt { get; private set; } = DateTime.Now;
    
    /// <summary>
    /// Planos de serviço associados ao cliente
    /// </summary>
    public virtual ICollection<ClientServicePlan> ClientServicePlans { get; set; } = new List<ClientServicePlan>();
    
    public virtual ICollection<ClientProfessionalReview> ClientProfessionalReviews { get; set; } = new List<ClientProfessionalReview>();
    
    /// <summary>
    /// Método para adicionar um plano de serviço ao cliente.
    /// </summary>
    /// <param name="servicePlan"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void AddServicePlan(ClientServicePlan servicePlan)
    {
        if (servicePlan == null)
            throw new ArgumentNullException(nameof(servicePlan), "Service plan cannot be null.");
        
        servicePlan.SetClient(this);
        ClientServicePlans.Add(servicePlan);
    }
    
    /// <summary>
    /// Método para adicionar uma avaliação de profissional feita pelo cliente.
    /// </summary>
    /// <param name="review"></param>
    /// <param name="professional"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public void AddReview(ClientProfessionalReview review, Professional professional)
    {
        if (review == null)
            throw new ArgumentNullException(nameof(review), "Review cannot be null.");
        
        review.SetClient(this);
        review.SetProfessional(professional);
        review.SetClientServicePlan(
            ClientServicePlans.FirstOrDefault(sp => sp.ServicePlanId == review.ClientServicePlanId) ??
            throw new InvalidOperationException("Client service plan not found."));
        ClientProfessionalReviews.Add(review);
    }
}