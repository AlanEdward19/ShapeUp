using TrainingService.Common.Interfaces;
using TrainingService.Exercises.Common.Repository;

namespace TrainingService.Exercises.DeleteExerciseById;

/// <summary>
/// Handler para deletar um exercício pelo ID
/// </summary>
/// <param name="repository"></param>
public class DeleteExerciseCommandHandler(IExerciseRepository repository)
    : IHandler<bool, DeleteExerciseByIdCommand>
{
    /// <summary>
    /// Método para deletar um exercício pelo ID.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(DeleteExerciseByIdCommand command, CancellationToken cancellationToken)
    {
        Exercise exercise = await repository.GetExerciseByIdAsync(command.ExerciseId, cancellationToken);
        
        await repository.DeleteAsync(exercise, cancellationToken);
        
        return true;
    }
}