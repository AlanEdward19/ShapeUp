using TrainingService.Exercises.Common.Enums;

namespace TrainingService.Exercises.GetExerciseById;

/// <summary>
/// Query para obter um exercício por Id.
/// </summary>
public class GetExerciseByIdQuery
{
    /// <summary>
    /// Id do exercício.
    /// </summary>
    public Guid ExerciseId { get;  private set; }
    
    /// <summary>
    /// método para setar o id do exercício.
    /// </summary>
    /// <param name="exerciseId"></param>
    public void SetExerciseId(Guid exerciseId)
    {
        ExerciseId = exerciseId;
    }
}