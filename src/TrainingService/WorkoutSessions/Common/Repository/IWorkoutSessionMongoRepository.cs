namespace TrainingService.WorkoutSessions.Common.Repository;

/// <summary>
/// Interface para o repositório de sessões de treino no MongoDB.
/// </summary>
public interface IWorkoutSessionMongoRepository
{
    /// <summary>
    /// Método para obter uma sessão de treino pelo ID da sessão.
    /// </summary>
    /// <param name="sessionId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WorkoutSession?> GetWorkoutSessionByIdAsync(string sessionId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Método para obter todas as sessões de treino de um usuário pelo ID do usuário.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ICollection<WorkoutSession>> GetWorkoutSessionsByUserIdAsync(string userId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Método para obter todas as sessões de treino associadas a um treino específico pelo ID do treino.
    /// </summary>
    /// <param name="workoutId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ICollection<WorkoutSession>> GetWorkoutSessionsByWorkoutIdIdAsync(Guid workoutId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Método para obter a sessão de treino atual de um usuário pelo ID do usuário.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WorkoutSession?> GetCurrentWorkoutSessionByUserIdAsync(string userId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Método para criar uma nova sessão de treino no MongoDB.
    /// </summary>
    /// <param name="session"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task CreateWorkoutSessionAsync(WorkoutSession session, CancellationToken cancellationToken);
    
    /// <summary>
    /// Método para deletar uma sessão de treino pelo ID da sessão.
    /// </summary>
    /// <param name="sessionId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteWorkoutSessionByIdAsync(string sessionId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Método para atualizar uma sessão de treino pelo ID da sessão.
    /// </summary>
    /// <param name="sessionId"></param>
    /// <param name="updatedSession"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UpdateWorkoutSessionByIdAsync(string sessionId, WorkoutSession updatedSession, CancellationToken cancellationToken);
}