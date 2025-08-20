using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TrainingService.WorkoutSessions.Common.Enums;
using TrainingService.WorkoutSessions.Common.ValueObjects;

namespace TrainingService.WorkoutSessions;

/// <summary>
/// Entidade que representa uma sessão de treino
/// </summary>
public class WorkoutSession
{
    /// <summary>
    /// Id da sessão de treino
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string SessionId { get; init; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// Id do usuário que está realizando o treino
    /// </summary>
    public string UserId { get; private set; }
    
    /// <summary>
    /// Id do treino
    /// </summary>
    public string WorkoutId { get; private set; }
    
    /// <summary>
    /// Data de início da sessão de treino
    /// </summary>
    public DateTime StartedAt { get; init; } = DateTime.UtcNow; 
    
    /// <summary>
    /// Data de término da sessão de treino
    /// </summary>
    public DateTime? EndedAt { get; set; }
    
    /// <summary>
    /// Status da sessão de treino
    /// </summary>
    public EWorkoutStatus Status { get; private set; }
    
    /// <summary>
    /// Exercícios da sessão de treino
    /// </summary>
    public List<WorkoutSessionExerciseValueObject> Exercises { get; private set; } = new List<WorkoutSessionExerciseValueObject>();

    /// <summary>
    /// Construtor padrão para o MongoDB
    /// </summary>
    public WorkoutSession() { }

    /// <summary>
    /// Construtor da sessão de treino
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="workoutId"></param>
    /// <param name="status"></param>
    /// <param name="exercises"></param>
    public WorkoutSession(string userId, Guid workoutId, EWorkoutStatus status, List<WorkoutSessionExerciseValueObject> exercises)
    {
        UserId = userId;
        WorkoutId = workoutId.ToString();
        Status = status;
        Exercises = exercises;
    }
    
    /// <summary>
    /// Método para adicionar exercícios à sessão de treino
    /// </summary>
    /// <param name="exercises"></param>
    public void AddExercises(List<WorkoutSessionExerciseValueObject> exercises)
    {
        Exercises.AddRange(exercises);
    }
    
    /// <summary>
    /// Método para atualizar os exercícios da sessão de treino
    /// </summary>
    /// <param name="exercises"></param>
    public void UpdateExercises(List<WorkoutSessionExerciseValueObject> exercises)
    {
        Exercises.Clear();
        Exercises.AddRange(exercises);
    }
    
    /// <summary>
    /// Método para atualizar o status da sessão de treino
    /// </summary>
    /// <param name="status"></param>
    public void UpdateStatus(EWorkoutStatus status)
    {
        Status = status;
        
        if (status is EWorkoutStatus.Finished or EWorkoutStatus.Canceled)
            EndedAt = DateTime.UtcNow;
        
        else
            EndedAt = null;
    }
    
}