using TrainingService.Exercises.Common.Enums;

namespace TrainingService.Exercises;

public class Exercise
{
    /// <summary>
    /// Identificador do exercício.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Nome do exercício.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Grupos musculares ao qual o exercício pertence.
    /// </summary>
    public EMuscleGroup MuscleGroups { get; set; }
    
    /// <summary>
    /// Se o exercício requer peso.
    /// </summary>
    public bool RequiresWeight { get; set; }
    
    /// <summary>
    /// Imagem do exercício.
    /// </summary>
    public string? ImageUrl { get; set; }
    
    /// <summary>
    /// Vídeo do exercício.
    /// </summary>
    public string? VideoUrl { get; set; }
    
    /// <summary>
    /// Data de criação do exercício.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Data de atualização do exercício.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}