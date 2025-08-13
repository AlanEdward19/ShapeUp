using TrainingService.Common.Interfaces;
using TrainingService.Exercises.Common.Repository;

namespace TrainingService.Exercises.DeleteExerciseById;

public class DeleteExerciseCommandHandler(IExerciseRepository repository)
    : IHandler<bool, DeleteExerciseByIdCommand>
{
    public async Task<bool> HandleAsync(DeleteExerciseByIdCommand command, CancellationToken cancellationToken)
    {
        Exercise? exercise = await repository.GetExerciseByIdAsync(command.ExerciseId, cancellationToken);
        ArgumentNullException.ThrowIfNull(exercise);
        
        await repository.DeleteAsync(exercise, cancellationToken);
        
        return true;
    }
}