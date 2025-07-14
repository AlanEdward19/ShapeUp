using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProfessionalManagementService.Professionals;

namespace ProfessionalManagementService.ServicePlans;

/// <summary>
/// Plano de serviço oferecido por um profissional
/// </summary>
public class ServicePlan
{
    /// <summary>
    /// Id do plano de serviço
    /// </summary>
    [Key]
    public Guid Id { get; private set; }
    
    /// <summary>
    /// Id do profissional associado ao plano de serviço
    /// </summary>
    public string ProfessionalId { get; private set; }
    
    /// <summary>
    /// Título do plano de serviço
    /// </summary>
    public string Title { get; private set; }
    
    /// <summary>
    /// Descrição do plano de serviço
    /// </summary>
    public string Description { get; private set; }
    
    /// <summary>
    /// Duração do plano de serviço em dias
    /// </summary>
    public int DurationInDays { get; private set; }
    
    /// <summary>
    /// Preço do plano de serviço
    /// </summary>
    public double Price { get; private set; }
    
    /// <summary>
    /// Data de criação do plano de serviço
    /// </summary>
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    
    /// <summary>
    /// Data de atualização do plano de serviço
    /// </summary>
    public DateTime UpdatedAt { get; private set; } = DateTime.Now;
    
    /// <summary>
    /// Profissional associado ao plano de serviço
    /// </summary>
    [ForeignKey(nameof(ProfessionalId))]
    public virtual Professional Professional { get; private set; }
    
    /// <summary>
    /// Método para definir o profissional associado ao plano de serviço.
    /// </summary>
    /// <param name="professional"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void SetProfessional(Professional professional) 
    {
        Professional = professional ?? throw new ArgumentNullException(nameof(professional), "Professional cannot be null.");
        ProfessionalId = professional.Id;
    }

    /// <summary>
    /// Construtor padrão para o EF Core
    /// </summary>
    public ServicePlan() { }

    /// <summary>
    /// Construtor para criar um novo plano de serviço
    /// </summary>
    /// <param name="professionalId"></param>
    /// <param name="title"></param>
    /// <param name="description"></param>
    /// <param name="durationInDays"></param>
    /// <param name="price"></param>
    public ServicePlan(string professionalId, string title, string description, int durationInDays, double price)
    {
        Id = Guid.NewGuid();
        ProfessionalId = professionalId;
        Title = title;
        Description = description;
        DurationInDays = durationInDays;
        Price = price;
    }
    
    /// <summary>
    /// Método para atualizar o titulo do plano de serviço
    /// </summary>
    /// <param name="title"></param>
    public void UpdateTitle(string? title)
    {
        if (string.IsNullOrWhiteSpace(title) ||
            title.ToLower().Replace(" ", "").Equals(Title.ToLower().Replace(" ", ""))) return;
        
        Title = title;
        UpdatedAt = DateTime.Now;
    }
    
    /// <summary>
    /// Método para atualizar a descrição do plano de serviço
    /// </summary>
    /// <param name="description"></param>
    public void UpdateDescription(string? description)
    {
        if (string.IsNullOrWhiteSpace(description) ||
            description.ToLower().Replace(" ", "").Equals(Description.ToLower().Replace(" ", ""))) return;
        
        Description = description;
        UpdatedAt = DateTime.Now;
    }
    
    /// <summary>
    /// Método para atualizar a duração do plano de serviço em dias
    /// </summary>
    /// <param name="durationInDays"></param>
    public void UpdateDurationInDays(int? durationInDays)
    {
        if (durationInDays == null || DurationInDays == durationInDays) return;
        
        DurationInDays = durationInDays.Value;
        UpdatedAt = DateTime.Now;
    }
    
    /// <summary>
    /// Método para atualizar o preço do plano de serviço
    /// </summary>
    /// <param name="price"></param>
    public void UpdatePrice(double? price)
    {
        if (price == null || Price.Equals(price)) return;
        
        Price = price.Value;
        UpdatedAt = DateTime.Now;
    }
}