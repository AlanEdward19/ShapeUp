namespace TrainingService.Workouts.DeleteWorkoutById;

/// <summary>
/// Comando para deletar um treino por id.
/// </summary>
public class DeleteWorkoutByIdCommand()
{
    /// <summary>
    /// Id do treino.
    /// </summary>
    public Guid WorkoutId { get; private set; }
    
    /// <summary>
    /// Método para setar o id do treino.
    /// </summary>
    /// <param name="workoutId"></param>
    public void SetWorkoutId(Guid workoutId)
    {
        WorkoutId = workoutId;
    }
}