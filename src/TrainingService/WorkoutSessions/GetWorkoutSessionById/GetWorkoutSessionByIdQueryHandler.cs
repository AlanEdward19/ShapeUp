using TrainingService.Common.Interfaces;
using TrainingService.WorkoutSessions.Common.Repository;

namespace TrainingService.WorkoutSessions.GetWorkoutSessionById;

public class GetWorkoutSessionByIdQueryHandler(IWorkoutSessionMongoRepository repository) : IHandler<WorkoutSession, GetWorkoutSessionByIdQuery>
{
    public async Task<WorkoutSession> HandleAsync(GetWorkoutSessionByIdQuery query, CancellationToken cancellationToken)
    {
        WorkoutSession? workoutSession = await repository.GetWorkoutSessionByIdAsync(query.SessionId, cancellationToken);
        
        ArgumentNullException.ThrowIfNull(workoutSession, $"Workout session with ID {query.SessionId} not found.");
        
        return workoutSession;
    }
}