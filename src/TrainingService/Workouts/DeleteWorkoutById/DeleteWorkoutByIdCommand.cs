namespace TrainingService.Workouts.DeleteWorkoutById;

/// <summary>
/// Comando para deletar um treino por id.
/// </summary>
/// <param name="id"></param>
public class DeleteWorkoutByIdCommand()
{
    /// <summary>
    /// Id do treino.
    /// </summary>
    public Guid WorkoutId { get; private set; }
    
    /// <summary>
    /// método para setar o id do treino.
    /// </summary>
    /// <param name="workoutId"></param>
    public void SetWorkoutId(Guid workoutId)
    {
        WorkoutId = workoutId;
    }
}