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
    public EMuscleGroup? MuscleGroup { get; private set; }
    
    /// <summary>
    /// Método para setar o grupo muscular do exercício.
    /// </summary>
    /// <param name="muscleGroup"></param>
    public void SetMuscleGroup(EMuscleGroup? muscleGroup)
    {
        MuscleGroup = muscleGroup;
    }
}