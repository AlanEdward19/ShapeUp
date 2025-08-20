using TrainingService.Workouts.Common.Enums;

namespace TrainingService.WorkoutSessions.Common.ValueObjects;

public class WorkoutSessionExerciseValueObject
{
    /// <summary>
    /// Id do exercício
    /// </summary>
    public string ExerciseId { get; set; }
    
    /// <summary>
    /// Peso do exercício
    /// </summary>
    public double? Weight { get; set; }
    
    /// <summary>
    /// Repetições do exercício
    /// </summary>
    public int? Repetitions { get; set; }
    
    /// <summary>
    /// Unidade de medida do exercício
    /// </summary>
    public EMeasureUnit MeasureUnit { get; set; }
}