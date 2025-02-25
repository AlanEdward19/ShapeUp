namespace TrainingService.WorkoutSessions.CreateWorkoutSession;

/// <summary>
/// Comando para criar uma sessão de treino.
/// </summary>
/// <param name="userId"></param>
/// <param name="workoutId"></param>
public class CreateWorkoutSessionCommand(Guid userId, Guid workoutId)
{
    /// <summary>
    /// Id do usuário que está realizando o treino
    /// </summary>
    public Guid UserId { get; set; } = userId;

    /// <summary>
    /// Id do treino
    /// </summary>
    public Guid WorkoutId { get; set; } = workoutId;
}