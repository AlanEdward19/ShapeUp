namespace TrainingService.Workouts.GetWorkoutById;

/// <summary>
/// Query para obter um treino por id.
/// </summary>
public class GetWorkoutByIdQuery
{
    /// <summary>
    /// Id do treino.
    /// </summary>
    public Guid WorkoutId { get;  private set; }
    
    /// <summary>
    /// método para setar o id do treino.
    /// </summary>
    /// <param name="workoutId"></param>
    public void SetWorkoutId(Guid workoutId)
    {
        WorkoutId = workoutId;
    }
}