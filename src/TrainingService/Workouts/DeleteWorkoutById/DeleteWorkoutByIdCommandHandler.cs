using TrainingService.Common.Interfaces;
using TrainingService.Workouts.Common.Repository;

namespace TrainingService.Workouts.DeleteWorkoutById;

public class DeleteWorkoutByIdCommandHandler(IWorkoutRepository repository)
    : IHandler<bool, DeleteWorkoutByIdCommand>
{
    public async Task<bool> HandleAsync(DeleteWorkoutByIdCommand command, CancellationToken cancellationToken)
    {
        Workout? workout = await repository.GetWorkoutAsync(command.WorkoutId, cancellationToken);
        ArgumentNullException.ThrowIfNull(workout);
        
        await repository.DeleteAsync(workout, cancellationToken);
        
        return true;
    }
}