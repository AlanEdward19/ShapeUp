namespace TrainingService.WorkoutSessions.DeleteWorkoutSessionById;

/// <summary>
/// Comando para deletar uma sessão de treino por id.
/// </summary>
public class DeleteWorkoutSessionByIdCommand
{
    /// <summary>
    /// Id da sessão de treino.
    /// </summary>
    public string SessionId { get; private set; }
    
    public void SetSessionId(Guid sessionId)
    {
        SessionId = sessionId.ToString();
    }
}


