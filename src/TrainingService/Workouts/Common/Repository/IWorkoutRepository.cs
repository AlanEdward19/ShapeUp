namespace TrainingService.Workouts.Common.Repository;

/// <summary>
/// Interface para o repositório de treinos.
/// </summary>
public interface IWorkoutRepository
{
    /// <summary>
    /// Método para obter um treino por ID.
    /// </summary>
    /// <param name="workoutId"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="track"></param>
    /// <returns></returns>
    Task<Workout?> GetWorkoutAsync(Guid workoutId, CancellationToken cancellationToken,bool track = false);
    
    /// <summary>
    /// Método para obter treinos pelo ID do usuário.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ICollection<Workout>> GetWorkoutsByUserIdAsync(string userId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Método para adicionar um novo treino.
    /// </summary>
    /// <param name="workout"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task AddAsync(Workout workout, CancellationToken cancellationToken);
    
    /// <summary>
    /// Método para deletar um treino existente.
    /// </summary>
    /// <param name="workout"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync(Workout workout, CancellationToken cancellationToken);
    
    /// <summary>
    /// Método para atualizar um treino existente.
    /// </summary>
    /// <param name="workout"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UpdateAsync(Workout workout, CancellationToken cancellationToken);
}