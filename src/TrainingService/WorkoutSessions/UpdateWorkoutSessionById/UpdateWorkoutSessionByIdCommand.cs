using TrainingService.WorkoutSessions.Common.Enums;
using TrainingService.WorkoutSessions.Common.ValueObjects;

namespace TrainingService.WorkoutSessions.UpdateWorkoutSessionById;

/// <summary>
/// Comando para atualizar uma sessão de treino por id.
/// </summary>
/// <param name="id"></param>
/// <param name="status"></param>
/// <param name="exercises"></param>
public class UpdateWorkoutSessionByIdCommand(
    Guid id,
    EWorkoutStatus? status,
    List<WorkoutSessionExerciseValueObject>? exercises)
{
    /// <summary>
    /// Id da sessão de treino
    /// </summary>
    public Guid Id { get; set; } = id;

    /// <summary>
    /// Status da sessão de treino
    /// </summary>
    public EWorkoutStatus? Status { get; set; } = status;

    /// <summary>
    /// Exercícios da sessão de treino
    /// </summary>
    public List<WorkoutSessionExerciseValueObject>? Exercises { get; set; } = exercises;
}