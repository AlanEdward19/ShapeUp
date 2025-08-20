using MongoDB.Driver;
using SharedKernel.Exceptions;
using TrainingService.WorkoutSessions.Common.Enums;

namespace TrainingService.WorkoutSessions.Common.Repository;

/// <summary>
/// Repositório para gerenciar sessões de treino no MongoDB.
/// </summary>
public class WorkoutSessionMongoRepository : IWorkoutSessionMongoRepository
{
    private readonly IMongoCollection<WorkoutSession> _collection;

    /// <summary>
    /// Construtor do repositório de sessões de treino.
    /// </summary>
    /// <param name="database"></param>
    public WorkoutSessionMongoRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<WorkoutSession>("WorkoutSessions");
    }

    /// <summary>
    /// Método para obter uma sessão de treino pelo ID da sessão.
    /// </summary>
    /// <param name="sessionId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<WorkoutSession?> GetWorkoutSessionByIdAsync(string sessionId, CancellationToken cancellationToken)
    {
        var filter = Builders<WorkoutSession>.Filter.Eq(x => x.SessionId, sessionId);
        return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }
    
    /// <summary>
    /// Método para obter todas as sessões de treino de um usuário pelo ID do usuário.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ICollection<WorkoutSession>> GetWorkoutSessionsByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        var filter = Builders<WorkoutSession>.Filter.Eq(x => x.UserId, userId);
        return await _collection.Find(filter).ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Método para obter todas as sessões de treino associadas a um treino específico pelo ID do treino.
    /// </summary>
    /// <param name="workoutId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ICollection<WorkoutSession>> GetWorkoutSessionsByWorkoutIdIdAsync(Guid workoutId, CancellationToken cancellationToken)
    {
        var filter = Builders<WorkoutSession>.Filter.Eq(x => x.WorkoutId, workoutId.ToString());
        return await _collection.Find(filter).ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Método para obter a sessão de treino atual de um usuário pelo ID do usuário.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<WorkoutSession?> GetCurrentWorkoutSessionByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        var filter = Builders<WorkoutSession>.Filter.And(
            Builders<WorkoutSession>.Filter.Eq(x => x.UserId, userId),
            Builders<WorkoutSession>.Filter.Eq(x => x.Status, EWorkoutStatus.InProgress)
        );

        return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Método para criar uma nova sessão de treino no MongoDB.
    /// </summary>
    /// <param name="session"></param>
    /// <param name="cancellationToken"></param>
    public async Task CreateWorkoutSessionAsync(WorkoutSession session, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(session, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Método para deletar uma sessão de treino pelo ID da sessão.
    /// </summary>
    /// <param name="sessionId"></param>
    /// <param name="cancellationToken"></param>
    public async Task DeleteWorkoutSessionByIdAsync(string sessionId, CancellationToken cancellationToken)
    {
        var filter = Builders<WorkoutSession>.Filter.Eq(x => x.SessionId, sessionId);
        var deleteResult  =await _collection.DeleteOneAsync(filter, cancellationToken);
        
        if (deleteResult.DeletedCount == 0)
            throw new NotFoundException($"Workout session with ID {sessionId} not found.");
    }

    /// <summary>
    /// Método para atualizar uma sessão de treino pelo ID da sessão.
    /// </summary>
    /// <param name="sessionId"></param>
    /// <param name="updatedSession"></param>
    /// <param name="cancellationToken"></param>
    public async Task UpdateWorkoutSessionByIdAsync(string sessionId, WorkoutSession updatedSession, CancellationToken cancellationToken)
    {
        var filter = Builders<WorkoutSession>.Filter.Eq(x => x.SessionId, sessionId);
        await _collection.ReplaceOneAsync(filter, updatedSession, cancellationToken: cancellationToken);
    }
}
