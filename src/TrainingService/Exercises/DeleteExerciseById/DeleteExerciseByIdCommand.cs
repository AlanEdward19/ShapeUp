namespace TrainingService.Exercises.DeleteExerciseById;

/// <summary>
/// Comando para deletar um exercício.
/// </summary>
public class DeleteExerciseByIdCommand()
{
    /// <summary>
    /// Id do exercício.
    /// </summary>
    public Guid ExerciseId { get; private set; }
    
    /// <summary>
    /// método para setar o id do exercício.
    /// </summary>
    /// <param name="exerciseId"></param>
    public void SetExerciseId(Guid exerciseId)
    {
        ExerciseId = exerciseId;
    }
}