using SharedKernel.Exceptions;
using TrainingService.Common.Interfaces;
using TrainingService.Workouts.Common.Repository;

namespace TrainingService.Workouts.DeleteWorkoutById;

/// <summary>
/// Handler para o comando de exclusão de treino por id.
/// </summary>
/// <param name="repository"></param>
public class DeleteWorkoutByIdCommandHandler(IWorkoutRepository repository)
    : IHandler<bool, DeleteWorkoutByIdCommand>
{
    /// <summary>
    /// Método para tratar o comando de exclusão de treino por id.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(DeleteWorkoutByIdCommand command, CancellationToken cancellationToken)
    {
        Workout? workout = await repository.GetWorkoutAsync(command.WorkoutId, cancellationToken, true);
        
        if (workout is null)
            throw new NotFoundException($"Workout with id '{command.WorkoutId}' not found.");
        
        await repository.DeleteAsync(workout, cancellationToken);
        
        return true;
    }
}