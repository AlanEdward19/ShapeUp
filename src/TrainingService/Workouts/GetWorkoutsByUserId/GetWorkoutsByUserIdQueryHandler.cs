using TrainingService.Common.Interfaces;
using TrainingService.Workouts.Common;
using TrainingService.Workouts.Common.Repository;

namespace TrainingService.Workouts.GetWorkoutsByUserId;

public class GetWorkoutsByUserIdQueryHandler(IWorkoutRepository repository) 
    : IHandler<ICollection<WorkoutDto>, GetWorkoutsByUserIdQuery>
{
    public async Task<ICollection<WorkoutDto>> HandleAsync(GetWorkoutsByUserIdQuery query, CancellationToken cancellationToken)
    {
        ICollection<Workout> workouts = await repository.GetWorkoutsByUserIdAsync(query.UserId, cancellationToken);
        
        return workouts
            .Select(workout => new WorkoutDto(workout))
            .ToList();
    }
}