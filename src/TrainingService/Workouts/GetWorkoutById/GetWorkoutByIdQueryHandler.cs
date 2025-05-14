using TrainingService.Common.Interfaces;
using TrainingService.Workouts.Common;
using TrainingService.Workouts.Common.Repository;

namespace TrainingService.Workouts.GetWorkoutById;

public class GetWorkoutByIdQueryHandler(IWorkoutRepository repository) 
    : IHandler<WorkoutDto, GetWorkoutByIdQuery>
{
    public async Task<WorkoutDto> HandleAsync(GetWorkoutByIdQuery query, CancellationToken cancellationToken)
    {
        Workout? workout = await repository.GetWorkoutAsync(query.WorkoutId, cancellationToken);
        ArgumentNullException.ThrowIfNull(workout, nameof(workout));
        
        return new WorkoutDto(workout);
    }
}