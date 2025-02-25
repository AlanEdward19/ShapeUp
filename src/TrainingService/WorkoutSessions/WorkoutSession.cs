using TrainingService.WorkoutSessions.Common.Enums;
using TrainingService.WorkoutSessions.Common.ValueObjects;

namespace TrainingService.WorkoutSessions;

public class WorkoutSession
{
    /// <summary>
    /// Id da sessão de treino
    /// </summary>
    public Guid SessionId { get; set; }
    
    /// <summary>
    /// Id do usuário que está realizando o treino
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Id do treino
    /// </summary>
    public Guid WorkoutId { get; set; }
    
    /// <summary>
    /// Data de início da sessão de treino
    /// </summary>
    public DateTime StartedAt { get; set; }
    
    /// <summary>
    /// Data de término da sessão de treino
    /// </summary>
    public DateTime? EndedAt { get; set; }
    
    /// <summary>
    /// Status da sessão de treino
    /// </summary>
    public EWorkoutStatus Status { get; set; }
    
    /// <summary>
    /// Exercícios da sessão de treino
    /// </summary>
    public List<WorkoutSessionExerciseValueObject> Exercises { get; set; }
}