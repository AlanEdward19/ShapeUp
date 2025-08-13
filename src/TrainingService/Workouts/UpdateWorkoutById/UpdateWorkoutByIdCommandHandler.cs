using TrainingService.Common.Interfaces;
using TrainingService.Exercises.Common.Repository;
using TrainingService.Workouts.Common;
using TrainingService.Workouts.Common.Repository;

namespace TrainingService.Workouts.UpdateWorkoutById;

public class UpdateWorkoutByIdCommandHandler(IWorkoutRepository repository, IExerciseRepository exerciseRepository)
    : IHandler<WorkoutDto, UpdateWorkoutByIdCommand>
{
    public async Task<WorkoutDto> HandleAsync(UpdateWorkoutByIdCommand command, CancellationToken cancellationToken)
    {
        Workout? workout = await repository.GetWorkoutAsync(command.GetWorkoutId(), cancellationToken);
        ArgumentNullException.ThrowIfNull(workout);
        
        workout.UpdateWorkout(command);
        
        if (command.Exercises.Any())
        {
            var exercises =
                (await exerciseRepository.GetExercisesByIdsAsync(command.Exercises.ToList(), cancellationToken))
                .ToList();
            
            workout.UpdateWorkoutExercises(exercises);
        }
        
        await repository.UpdateAsync(workout, cancellationToken);
        
        return new WorkoutDto(workout);
    }
}