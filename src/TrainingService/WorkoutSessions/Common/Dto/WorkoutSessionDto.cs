using TrainingService.Exercises;
using TrainingService.WorkoutSessions.Common.Enums;

namespace TrainingService.WorkoutSessions.Common.Dto;

/// <summary>
/// DTO para representar uma sessão de treino.
/// </summary>
/// <param name="workoutSession"></param>
/// <param name="exercises"></param>
public class WorkoutSessionDto(WorkoutSession workoutSession, ICollection<Exercise> exercises)
{
    /// <summary>
    /// Id da sessão de treino.
    /// </summary>
    public string SessionId { get; private set; } = workoutSession.SessionId;
    
    /// <summary>
    /// Id do usuário que criou a sessão de treino.
    /// </summary>
    public string UserId { get; private set; } = workoutSession.UserId;
    
    /// <summary>
    /// Id do treino associado a esta sessão de treino.
    /// </summary>
    public string WorkoutId { get; private set; } = workoutSession.WorkoutId;
    
    /// <summary>
    /// Data e hora em que a sessão de treino foi iniciada.
    /// </summary>
    public DateTime StartedAt { get; private set; } = workoutSession.StartedAt;
    
    /// <summary>
    /// Data e hora em que a sessão de treino foi finalizada, se aplicável.
    /// </summary>
    public DateTime? EndedAt { get; private set; } = workoutSession.EndedAt;
    
    /// <summary>
    /// Status da sessão de treino, indicando se está em andamento, concluída ou cancelada.
    /// </summary>
    public EWorkoutStatus Status { get; private set; } = workoutSession.Status;
    
    /// <summary>
    /// Lista de exercícios associados a esta sessão de treino, cada um representado por um DTO que inclui detalhes do exercício e do desempenho na sessão.
    /// </summary>
    public List<WorkoutSessionExerciseDto> Exercises { get; private set; } = workoutSession.Exercises.Zip(exercises)
        .Select(pair => new WorkoutSessionExerciseDto(pair.First, pair.Second))
        .ToList();
}