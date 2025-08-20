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
    
    /// <summary>
    /// Id do usuário logado que está realizando a ação de deletar a sessão de treino.
    /// </summary>
    public string UserId { get; private set; }
    
    /// <summary>
    /// Método para definir o id do usuário logado que está realizando a ação de deletar a sessão de treino.
    /// </summary>
    /// <param name="userId"></param>
    public void SetUserId(string userId)
    {
        UserId = userId;
    }
    
    /// <summary>
    /// Método para definir o id da sessão de treino que será deletada.
    /// </summary>
    /// <param name="sessionId"></param>
    public void SetSessionId(Guid sessionId)
    {
        SessionId = sessionId.ToString();
    }
}


