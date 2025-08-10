using System.ComponentModel.DataAnnotations;
using ProfessionalManagementService.Common.Enums;
using ProfessionalManagementService.Professionals;
using ProfessionalManagementService.Reviews;
using ProfessionalManagementService.ServicePlans;

namespace ProfessionalManagementService.Clients;

/// <summary>
/// Cliente do serviço de gerenciamento profissional
/// </summary>
public class Client
{
    /// <summary>
    /// Id do cliente
    /// </summary>
    [Key]
    public string Id { get; private set; }
    
    /// <summary>
    /// Nome do cliente
    /// </summary>
    public string Name { get; private set; }
    
    /// <summary>
    /// Email do cliente
    /// </summary>
    public string Email { get; private set; }
    
    /// <summary>
    /// Data de criação do cliente
    /// </summary>
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    
    /// <summary>
    /// Data da última atualização do cliente
    /// </summary>
    public DateTime UpdatedAt { get; private set; } = DateTime.Now;
    
    /// <summary>
    /// Planos de serviço associados ao cliente
    /// </summary>
    public virtual ICollection<ClientServicePlan> ClientServicePlans { get; set; } = new List<ClientServicePlan>();
    
    /// <summary>
    /// Reviews feitas pelo cliente sobre profissionais
    /// </summary>
    public virtual ICollection<ClientProfessionalReview> ClientProfessionalReviews { get; set; } = new List<ClientProfessionalReview>();

    /// <summary>
    /// Construtor para o EF Core
    /// </summary>
    public Client() { }

    /// <summary>
    /// Construtor com paramêtros
    /// </summary>
    /// <param name="id"></param>
    /// <param name="email"></param>
    /// <param name="name"></param>
    public Client(string id, string email, string name)
    {
        Id = id;
        Email = email;
        Name = name;
    }

    /// <summary>
    /// Método para atualizar o email de um cliente.
    /// </summary>
    /// <param name="email"></param>
    public void UpdateEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email) ||
            email.ToLower().Replace(" ", "").Equals(Email.ToLower().Replace(" ", "")))
            return;
        
        Email = email;
        UpdatedAt = DateTime.Now;
    }
    
    public void UpdateName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name) ||
            name.ToLower().Replace(" ", "").Equals(Name.ToLower().Replace(" ", "")))
            return;
        
        Name = name;
        UpdatedAt = DateTime.Now;
    }

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
    
    public void RemoveServicePlan(ClientServicePlan servicePlan)
    {
        if (servicePlan == null)
            throw new ArgumentNullException(nameof(servicePlan), "Service plan cannot be null.");
        
        if (!ClientServicePlans.Remove(servicePlan))
            throw new InvalidOperationException("Service plan not found in client's service plans.");
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