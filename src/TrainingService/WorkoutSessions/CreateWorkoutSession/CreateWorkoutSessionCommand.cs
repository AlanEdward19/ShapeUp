using TrainingService.WorkoutSessions.Common.ValueObjects;

namespace TrainingService.WorkoutSessions.CreateWorkoutSession;

/// <summary>
/// Comando para criar uma sessão de treino.
/// </summary>
/// <param name="workoutId"></param>
public class CreateWorkoutSessionCommand(Guid workoutId, List<WorkoutSessionExerciseValueObject> exercises)
{
    /// <summary>
    /// Id do usuário que está realizando o treino
    /// </summary>
    private string UserId { get; set; }
    
    /// <summary>
    /// Método para definir o Id do usuário
    /// </summary>
    /// <param name="userId"></param>
    public void SetUserId(string userId) => UserId = userId;
    
    /// <summary>
    /// Método para obter o Id do usuário
    /// </summary>
    /// <returns></returns>
    public string GetUserId() => UserId;

    /// <summary>
    /// Id do treino
    /// </summary>
    public Guid WorkoutId { get; set; } = workoutId;

    /// <summary>
    /// Lista de exercícios do treino
    /// </summary>
    public List<WorkoutSessionExerciseValueObject> Exercises { get; set; } = exercises;
}