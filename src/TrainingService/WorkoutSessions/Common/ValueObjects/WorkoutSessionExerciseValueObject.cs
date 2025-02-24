namespace TrainingService.WorkoutSessions.Common.ValueObjects;

public class WorkoutSessionExerciseValueObject
{
    /// <summary>
    /// Id do exercício
    /// </summary>
    public Guid ExerciseId { get; set; }
    
    /// <summary>
    /// Peso do exercício
    /// </summary>
    public int? Weight { get; set; }
    
    /// <summary>
    /// Repetições do exercício
    /// </summary>
    public int? Repetitions { get; set; }
}