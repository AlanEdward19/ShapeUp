using TrainingService.Common.Interfaces;
using TrainingService.WorkoutSessions.Common.Repository;
using TrainingService.WorkoutSessions.GetWorkoutSessionByUserId;

namespace TrainingService.WorkoutSessions.GetWorkoutSessionById;

public class GetWorkoutSessionsByUserIdQueryHandler(IWorkoutSessionMongoRepository repository) : IHandler<ICollection<WorkoutSession>, GetWorkoutSessionsByUserIdQuery>
{
    public async Task<ICollection<WorkoutSession>> HandleAsync(GetWorkoutSessionsByUserIdQuery query, CancellationToken cancellationToken)
    {
        var workoutSessions = await repository.GetWorkoutSessionsByUserIdAsync(query.UserId, cancellationToken);
        
        return workoutSessions;
    }
}