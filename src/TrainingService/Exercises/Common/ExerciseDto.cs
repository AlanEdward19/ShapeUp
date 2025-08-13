using TrainingService.Exercises.Common.Enums;

namespace TrainingService.Exercises.Common;

public class ExerciseDto(Exercise exercise)
{
    /// <summary>
    /// Identificador do exercício.
    /// </summary>
    public Guid Id { get; private set; } = exercise.Id;
    
    /// <summary>
    /// Nome do exercício.
    /// </summary>
    public string Name { get; private set; } = exercise.Name;
    
    /// <summary>
    /// Grupos musculares ao qual o exercício pertence.
    /// </summary>
    public ICollection<EMuscleGroup> MuscleGroups { get; private set; } = GetMuscleGroups(exercise.MuscleGroups);
    
    /// <summary>
    /// Se o exercício requer peso.
    /// </summary>
    public bool? RequiresWeight { get; private set; } = exercise.RequiresWeight;
    
    /// <summary>
    /// Imagem do exercício.
    /// </summary>
    public string? ImageUrl { get; private set; } = exercise.ImageUrl;
    
    /// <summary>
    /// Vídeo do exercício.
    /// </summary>
    public string? VideoUrl { get; private set; } = exercise.VideoUrl;
    
    /// <summary>
    /// Data de criação do exercício.
    /// </summary>
    public DateTime CreatedAt { get; private set; } = exercise.CreatedAt;
    
    /// <summary>
    /// Data de atualização do exercício.
    /// </summary>
    public DateTime UpdatedAt { get; private set; } = exercise.UpdatedAt;
    
    public static ICollection<EMuscleGroup> GetMuscleGroups(EMuscleGroup muscleGroup)
    {
        return Enum.GetValues(typeof(EMuscleGroup))
            .Cast<EMuscleGroup>()
            .Where(flag => flag != 0 && muscleGroup.HasFlag(flag))
            .ToList();
    }
}