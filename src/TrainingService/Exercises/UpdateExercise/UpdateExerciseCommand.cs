using TrainingService.Exercises.Common.Enums;

namespace TrainingService.Exercises.UpdateExercise;

/// <summary>
/// Comando para atualizar um exercício.
/// </summary>
/// <param name="id"></param>
/// <param name="name"></param>
/// <param name="muscleGroups"></param>
/// <param name="requiresWeight"></param>
public class UpdateExerciseCommand(Guid id, string? name, IEnumerable<EMuscleGroup>? muscleGroups, bool? requiresWeight, string? imageUrl, string? videoUrl)
{
    /// <summary>
    /// Id do exercício.
    /// </summary>
    public Guid Id { get; set; } = id;

    /// <summary>
    /// Nome do exercício.
    /// </summary>
    public string? Name { get; set; } = name;

    /// <summary>
    /// Grupos musculares ao qual o exercício pertence.
    /// </summary>
    public IEnumerable<EMuscleGroup>? MuscleGroups { get; set; } = muscleGroups;

    /// <summary>
    /// Se o exercício requer peso.
    /// </summary>
    public bool? RequiresWeight { get; set; } = requiresWeight;

    /// <summary>
    /// Imaegm do exercício.
    /// </summary>
    public string? ImageUrl { get; set; } = imageUrl;

    /// <summary>
    /// Video do exercício.
    /// </summary>
    public string? VideoUrl { get; set; } = videoUrl;
}