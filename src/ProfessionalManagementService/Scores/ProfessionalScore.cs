namespace ProfessionalManagementService.Scores;

/// <summary>
/// Pontuação do profissional baseada nas avaliações dos clientes
/// </summary>
public class ProfessionalScore
{
    /// <summary>
    /// Id do profissional
    /// </summary>
    public string ProfessionalId { get; private set; }
    
    /// <summary>
    /// Valor médio das avaliações do profissional
    /// </summary>
    public double AverageScore { get; private set; }
    
    /// <summary>
    /// Quantidade total de avaliações do profissional
    /// </summary>
    public int TotalReviews { get; private set; }
    
    /// <summary>
    /// Data da última atualização do score do profissional
    /// </summary>
    public DateTime LastUpdated { get; private set; }
}