using TrainingService.WorkoutSessions.Common.Enums;
using TrainingService.WorkoutSessions.Common.ValueObjects;

namespace TrainingService.WorkoutSessions;

public class WorkoutSession
{
    /// <summary>
    /// Id da sessão de treino
    /// </summary>
    public string SessionId { get; init; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// Id do usuário que está realizando o treino
    /// </summary>
    public string UserId { get; private set; }
    
    /// <summary>
    /// Id do treino
    /// </summary>
    public Guid WorkoutId { get; private set; }
    
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

    public WorkoutSession() { }

    public WorkoutSession(string userId, Guid workoutId, EWorkoutStatus status, List<WorkoutSessionExerciseValueObject> exercises)
    {
        UserId = userId;
        WorkoutId = workoutId;
        Status = status;
        Exercises = exercises;
    }
    
    public void AddExercises(List<WorkoutSessionExerciseValueObject> exercises)
    {
        Exercises.AddRange(exercises);
    }
    
    public void UpdateExercises(List<WorkoutSessionExerciseValueObject> exercises)
    {
        Exercises.Clear();
        Exercises.AddRange(exercises);
    }
    
    public void AddExercise(WorkoutSessionExerciseValueObject exercise)
    {
        Exercises.Add(exercise);
    }

    public void RemoveExercise(WorkoutSessionExerciseValueObject exercise)
    {
        Exercises.Remove(exercise);
    }
    
    public void ClearExercises()
    {
        Exercises.Clear();
    }
    
    public void UpdateStatus(EWorkoutStatus status)
    {
        Status = status;
    }
    
}