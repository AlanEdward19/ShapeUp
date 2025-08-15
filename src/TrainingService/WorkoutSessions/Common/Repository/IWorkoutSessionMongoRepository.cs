namespace TrainingService.WorkoutSessions.Common.Repository;

public interface IWorkoutSessionMongoRepository
{
    Task<WorkoutSession?> GetWorkoutSessionByIdAsync(string sessionId, CancellationToken cancellationToken);
    
    Task<ICollection<WorkoutSession>> GetWorkoutSessionsByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task<ICollection<WorkoutSession>> GetWorkoutSessionsByWorkoutIdIdAsync(Guid workoutId, CancellationToken cancellationToken);
    
    Task<WorkoutSession?> GetCurrentWorkoutSessionByUserIdAsync(string userId, CancellationToken cancellationToken);
    
    Task CreateWorkoutSessionAsync(WorkoutSession session, CancellationToken cancellationToken);
    
    Task DeleteWorkoutSessionByIdAsync(string sessionId, CancellationToken cancellationToken);
    
    Task UpdateWorkoutSessionByIdAsync(string sessionId, WorkoutSession updatedSession, CancellationToken cancellationToken);
}