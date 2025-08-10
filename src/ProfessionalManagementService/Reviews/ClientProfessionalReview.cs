using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProfessionalManagementService.Clients;
using ProfessionalManagementService.Professionals;

namespace ProfessionalManagementService.Reviews;

/// <summary>
/// Review feita por um cliente sobre um profissional
/// </summary>
public class ClientProfessionalReview
{
    /// <summary>
    /// Id da avaliação do profissional pelo cliente
    /// </summary>
    [Key]
    public Guid Id { get; private set; }
    
    /// <summary>
    /// Id do cliente que fez a avaliação
    /// </summary>
    public string ClientId { get; private set; }
    
    /// <summary>
    /// Id do profissional avaliado
    /// </summary>
    public string? ProfessionalId { get; private set; }
    
    /// <summary>
    /// Id do plano de serviço associado à avaliação
    /// </summary>
    public Guid? ClientServicePlanId { get; private set; }
    
    /// <summary>
    /// Avaliação do profissional pelo cliente
    /// </summary>
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
    public int Rating { get; private set; }
    
    /// <summary>
    /// Comentário do cliente sobre o profissional
    /// </summary>
    public string? Comment { get; private set; }
    
    /// <summary>
    /// Data de criação da avaliação
    /// </summary>
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    
    /// <summary>
    /// Data de atualização da avaliação
    /// </summary>
    public DateTime UpdatedAt { get; private set; } = DateTime.Now;
    
    /// <summary>
    /// Cliente que fez a avaliação
    /// </summary>
    [ForeignKey(nameof(ClientId))]
    public virtual Client Client { get; private set; }
    
    /// <summary>
    /// Profissional avaliado
    /// </summary>
    [ForeignKey(nameof(ProfessionalId))]
    public virtual Professional? Professional { get; private set; }
    
    /// <summary>
    /// Plano de serviço do cliente associado à avaliação
    /// </summary>
    [ForeignKey(nameof(ClientServicePlanId))]
    public virtual ClientServicePlan? ClientServicePlan { get; private set; }
    
    /// <summary>
    /// Método para definir o cliente que fez a avaliação.
    /// </summary>
    /// <param name="client"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void SetClient(Client client)
    {
        Client = client ?? throw new ArgumentNullException(nameof(client), "Client cannot be null.");
        ClientId = client.Id;
    }
    
    /// <summary>
    /// Método para definir o profissional avaliado.
    /// </summary>
    /// <param name="professional"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void SetProfessional(Professional professional)
    {
        Professional = professional ?? throw new ArgumentNullException(nameof(professional), "Professional cannot be null.");
        ProfessionalId = professional.Id;
    }
    
    /// <summary>
    /// Método para definir o plano de serviço do cliente associado à avaliação.
    /// </summary>
    /// <param name="clientServicePlan"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void SetClientServicePlan(ClientServicePlan clientServicePlan)
    {
        ClientServicePlan = clientServicePlan ?? throw new ArgumentNullException(nameof(clientServicePlan), "Client service plan cannot be null.");
        ClientServicePlanId = clientServicePlan.Id;
    }
    
    /// <summary>
    /// Método para remover o plano de serviço do cliente associado à avaliação.
    /// </summary>
    public void RemoveClientServicePlan()
    {
        ClientServicePlan = null;
        ClientServicePlanId = null;
    }
    
    public void UpdateRating(int? rating)
    {
        if (rating == null || rating == Rating)
            return;
        
        if (rating is < 1 or > 5)
            throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 1 and 5.");
        
        Rating = rating.Value;
        UpdatedAt = DateTime.Now;
    }
    
    public void UpdateComment(string? comment)
    {
        if (comment == null || comment.Equals(Comment))
            return;

        Comment = comment;
        UpdatedAt = DateTime.Now;
    }
    
    /// <summary>
    /// Constructor padrão para o EF Core
    /// </summary>
    public ClientProfessionalReview() { }

    /// <summary>
    /// Constructor para criar uma nova avaliação de profissional pelo cliente.
    /// </summary>
    /// <param name="rating"></param>
    /// <param name="comment"></param>
    public ClientProfessionalReview(int rating, string? comment)
    {
        Id = Guid.NewGuid();
        Rating = rating;
        Comment = comment;
    }
}