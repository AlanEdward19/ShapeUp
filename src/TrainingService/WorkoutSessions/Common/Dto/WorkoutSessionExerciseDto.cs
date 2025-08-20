using TrainingService.Exercises;
using TrainingService.Exercises.Common;
using TrainingService.Workouts.Common.Enums;
using TrainingService.WorkoutSessions.Common.ValueObjects;

namespace TrainingService.WorkoutSessions.Common.Dto;

/// <summary>
/// DTO para representar um exercício em uma sessão de treino.
/// </summary>
/// <param name="workoutSessionExerciseValueObject"></param>
/// <param name="exercise"></param>
public class WorkoutSessionExerciseDto(WorkoutSessionExerciseValueObject workoutSessionExerciseValueObject, Exercise exercise)
{
    /// <summary>
    /// Quantidade de peso do exercício
    /// </summary>
    public double? Weight { get; set; } = workoutSessionExerciseValueObject.Weight;
    
    /// <summary>
    /// Quantidade de repetições do exercício
    /// </summary>
    public int? Repetitions { get; set; } = workoutSessionExerciseValueObject.Repetitions;
    
    /// <summary>
    /// Unidade de medida do exercício
    /// </summary>
    public EMeasureUnit MeasureUnit { get; set; } = workoutSessionExerciseValueObject.MeasureUnit;

    /// <summary>
    /// Metadados do exercício.
    /// </summary>
    public ExerciseDto Metadata { get; set; } = new(exercise);
}