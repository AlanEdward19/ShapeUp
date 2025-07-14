namespace ProfessionalManagementService.Scores;

/// <summary>
/// Pontuação do profissional baseada nas avaliações dos clientes
/// </summary>
public class ProfessionalScore(string professionalId, double averageScore, int totalReviews, DateTime lastUpdated)
{
    /// <summary>
    /// Id do profissional
    /// </summary>
    public string ProfessionalId { get; private set; } = professionalId;
    
    /// <summary>
    /// Valor médio das avaliações do profissional
    /// </summary>
    public double AverageScore { get; private set; } = averageScore;
    
    /// <summary>
    /// Quantidade total de avaliações do profissional
    /// </summary>
    public int TotalReviews { get; private set; } = totalReviews;
    
    /// <summary>
    /// Data da última atualização do score do profissional
    /// </summary>
    public DateTime LastUpdated { get; private set; } = lastUpdated;
}