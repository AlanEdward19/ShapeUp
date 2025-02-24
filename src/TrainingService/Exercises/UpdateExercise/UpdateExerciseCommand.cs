using TrainingService.Exercises.Common.Enums;

namespace TrainingService.Exercises.UpdateExercise;

/// <summary>
/// Comando para atualizar um exercício.
/// </summary>
/// <param name="id"></param>
/// <param name="name"></param>
/// <param name="muscleGroups"></param>
/// <param name="requiresWeight"></param>
public class UpdateExerciseCommand(Guid id, string? name, IEnumerable<EMuscleGroup>? muscleGroups, bool? requiresWeight)
{
    /// <summary>
    /// Id do exercício.
    /// </summary>
    public Guid Id { get; set; }

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
}