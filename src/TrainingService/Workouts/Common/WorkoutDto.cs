using TrainingService.Exercises.Common;
using TrainingService.Workouts.Common.Enums;

namespace TrainingService.Workouts.Common;

/// <summary>
/// DTO para representar um treino.
/// </summary>
/// <param name="workout"></param>
public class WorkoutDto(Workout workout)
{
    /// <summary>
    /// Id do treino.
    /// </summary>
    public Guid Id { get; private set; } = workout.Id;
    
    /// <summary>
    /// Id do usuário que criou o treino.
    /// </summary>
    public string CreatorId { get; private set; } = workout.CreatorId;
    
    /// <summary>
    /// Id do usuário para o qual o treino foi criado.
    /// </summary>
    public string? UserId { get; private set; } = workout.UserId;
    
    /// <summary>
    /// Nome do treino.
    /// </summary>
    public string Name { get; private set; } = workout.Name;

    /// <summary>
    /// Visibilidade do treino.
    /// </summary>
    public EWorkoutVisibility Visibility { get; private set; } = workout.Visibility;
    
    /// <summary>
    /// Exercícios do treino.
    /// </summary>
    public ICollection<ExerciseDto> Exercises { get; private set; } = workout.Exercises.Select(x => new ExerciseDto(x)).ToList();
    
    /// <summary>
    /// Tempo de descanso entre os exercícios, em segundos.
    /// </summary>
    public int RestingTimeInSeconds { get; private set; } = workout.RestingTimeInSeconds;
}