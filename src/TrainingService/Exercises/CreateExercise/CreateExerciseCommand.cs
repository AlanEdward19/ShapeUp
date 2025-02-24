using TrainingService.Exercises.Common.Enums;

namespace TrainingService.Exercises.CreateExercise;

/// <summary>
/// Comando para criar um exercício.
/// </summary>
/// <param name="name"></param>
/// <param name="muscleGroups"></param>
/// <param name="requiresWeight"></param>
public class CreateExerciseCommand(string name, IEnumerable<EMuscleGroup> muscleGroups, bool requiresWeight)
{
    /// <summary>
    /// Nome do exercício.
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// Grupos musculares ao qual o exercício pertence.
    /// </summary>
    public IEnumerable<EMuscleGroup> MuscleGroups { get; set; } = muscleGroups;

    /// <summary>
    /// Se o exercício requer peso.
    /// </summary>
    public bool RequiresWeight { get; set; } = requiresWeight;
}