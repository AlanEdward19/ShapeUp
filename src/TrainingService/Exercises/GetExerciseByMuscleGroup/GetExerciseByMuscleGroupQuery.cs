using TrainingService.Exercises.Common.Enums;

namespace TrainingService.Exercises.GetExerciseByMuscleGroup;

/// <summary>
/// Query para obter um exercício por grupo muscular.
/// </summary>
public class GetExerciseByMuscleGroupQuery
{
    /// <summary>
    /// Grupo muscular.
    /// </summary>
    public EMuscleGroup MuscleGroup { get; set; }
}