using MongoDB.Driver;
using TrainingService.WorkoutSessions.Common.Enums;

namespace TrainingService.WorkoutSessions.Common.Repository;

public class WorkoutSessionMongoRepository : IWorkoutSessionMongoRepository
{
    private readonly IMongoCollection<WorkoutSession> _collection;

    public WorkoutSessionMongoRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<WorkoutSession>("WorkoutSessions");
    }

    public async Task<WorkoutSession> GetWorkoutSessionByIdAsync(string sessionId, CancellationToken cancellationToken)
    {
        var filter = Builders<WorkoutSession>.Filter.Eq(x => x.SessionId, sessionId);
        return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }
    
    public async Task<ICollection<WorkoutSession>?> GetWorkoutSessionsByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        var filter = Builders<WorkoutSession>.Filter.Eq(x => x.UserId, userId);
        return await _collection.Find(filter).ToListAsync(cancellationToken);
    }

    public async Task<WorkoutSession?> GetCurrentWorkoutSessionByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        var filter = Builders<WorkoutSession>.Filter.And(
            Builders<WorkoutSession>.Filter.Eq(x => x.UserId, userId),
            Builders<WorkoutSession>.Filter.Eq(x => x.Status, EWorkoutStatus.InProgress)
        );

        return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task CreateWorkoutSessionAsync(WorkoutSession session, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(session, cancellationToken: cancellationToken);
    }

    public async Task DeleteWorkoutSessionByIdAsync(string sessionId, CancellationToken cancellationToken)
    {
        var filter = Builders<WorkoutSession>.Filter.Eq(x => x.SessionId, sessionId);
        await _collection.DeleteOneAsync(filter, cancellationToken);
    }

    public async Task UpdateWorkoutSessionByIdAsync(string sessionId, WorkoutSession updatedSession, CancellationToken cancellationToken)
    {
        var filter = Builders<WorkoutSession>.Filter.Eq(x => x.SessionId, sessionId);
        await _collection.ReplaceOneAsync(filter, updatedSession, cancellationToken: cancellationToken);
    }
}
