using TrainingService.Common.Interfaces;
using TrainingService.Exercises.Common.Repository;
using TrainingService.Workouts.Common.Repository;

namespace TrainingService.Workouts.UpdateWorkoutById;

public class UpdateWorkoutByIdCommandHandler(IWorkoutRepository repository, IExerciseRepository exerciseRepository)
    : IHandler<bool, UpdateWorkoutByIdCommand>
{
    public async Task<bool> HandleAsync(UpdateWorkoutByIdCommand command, CancellationToken cancellationToken)
    {
        Workout? workout = await repository.GetWorkoutAsync(command.Id, cancellationToken);
        ArgumentNullException.ThrowIfNull(workout);
        
        workout.UpdateWorkout(command);
        
        if (command.Exercises.Any())
        {
            var exercises = (await Task.WhenAll(command.Exercises.Select(x => exerciseRepository.GetExerciseAsync(x, cancellationToken))))
                .Where(x => x != null)
                .Select(x => x!)
                .ToList();
            
            workout.UpdateWorkoutExercises(exercises);
        }
        
        await repository.UpdateAsync(workout, cancellationToken);
        
        return true;
    }
}