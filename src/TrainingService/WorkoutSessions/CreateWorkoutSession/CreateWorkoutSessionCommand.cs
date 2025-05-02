using TrainingService.WorkoutSessions.Common.ValueObjects;

namespace TrainingService.WorkoutSessions.CreateWorkoutSession;

/// <summary>
/// Comando para criar uma sessão de treino.
/// </summary>
/// <param name="userId"></param>
/// <param name="workoutId"></param>
public class CreateWorkoutSessionCommand(string userId, Guid workoutId, List<WorkoutSessionExerciseValueObject> exercises)
{
    /// <summary>
    /// Id do usuário que está realizando o treino
    /// </summary>
    public string UserId { get; set; } = userId;

    /// <summary>
    /// Id do treino
    /// </summary>
    public Guid WorkoutId { get; set; } = workoutId;

    /// <summary>
    /// Lista de exercícios do treino
    /// </summary>
    public List<WorkoutSessionExerciseValueObject> Exercises { get; set; } = exercises;
}