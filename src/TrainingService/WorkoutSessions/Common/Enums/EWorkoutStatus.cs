namespace TrainingService.WorkoutSessions.Common.Enums;

/// <summary>
/// Status da sessão de treino
/// </summary>
public enum EWorkoutStatus
{
    /// <summary>
    ///     Sessão de treino em andamento
    /// </summary>
    InProgress,

    /// <summary>
    ///     Sessão de treino finalizada
    /// </summary>
    Finished,

    /// <summary>
    ///     Sessão de treino cancelada
    /// </summary>
    Canceled
}