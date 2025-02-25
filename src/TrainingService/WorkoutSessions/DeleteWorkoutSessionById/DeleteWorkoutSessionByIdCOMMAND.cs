namespace TrainingService.WorkoutSessions.DeleteWorkoutSessionById;

/// <summary>
/// Comando para deletar uma sessão de treino por id.
/// </summary>
public class DeleteWorkoutSessionByIdCOMMAND
{
    /// <summary>
    /// Id da sessão de treino.
    /// </summary>
    public Guid Id { get; set; }
}