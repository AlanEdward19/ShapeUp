using TrainingService.Exercises.Common.Enums;

namespace TrainingService.Exercises.CreateExercise;

/// <summary>
/// Comando para criar um exercício.
/// </summary>
/// <param name="name"></param>
/// <param name="muscleGroups"></param>
/// <param name="requiresWeight"></param>
public class CreateExerciseCommand
{
    /// <summary>
    /// Nome do exercício.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Grupos musculares ao qual o exercício pertence.
    /// </summary>
    public IEnumerable<EMuscleGroup> MuscleGroups { get; set; }

    /// <summary>
    /// Se o exercício requer peso.
    /// </summary>
    public bool RequiresWeight { get; set; }
}