namespace TrainingService.WorkoutSessions.GetWorkoutSessionsByWorkoutId;

/// <summary>
/// Query para obter todas as sessões de treino de um treino pelo ID do treino.
/// </summary>
/// <param name="workoutId"></param>
public class GetWorkoutSessionsByWorkoutIdQuery(Guid workoutId)
{
    /// <summary>
    /// Id do treino cujas sessões de treino serão obtidas.
    /// </summary>
    public Guid WorkoutId { get; private set; } = workoutId;
}