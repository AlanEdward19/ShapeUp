namespace TrainingService.WorkoutSessions.Common.Repository;

public interface IWorkoutSessionMongoRepository
{
    Task<WorkoutSession?> GetWorkoutSessionByIdAsync(string sessionId, CancellationToken cancellationToken);
    
    Task CreateWorkoutSessionAsync(WorkoutSession session, CancellationToken cancellationToken);
    
    Task DeleteWorkoutSessionByIdAsync(string sessionId, CancellationToken cancellationToken);
    
    Task UpdateWorkoutSessionByIdAsync(string sessionId, WorkoutSession updatedSession, CancellationToken cancellationToken);
}