using SharedKernel.Exceptions;
using TrainingService.Common.Interfaces;
using TrainingService.Workouts.Common;
using TrainingService.Workouts.Common.Repository;

namespace TrainingService.Workouts.GetWorkoutById;

/// <summary>
/// Handler para a query de obtenção de treino por id.
/// </summary>
/// <param name="repository"></param>
public class GetWorkoutByIdQueryHandler(IWorkoutRepository repository) 
    : IHandler<WorkoutDto, GetWorkoutByIdQuery>
{
    /// <summary>
    /// Método para tratar a query de obtenção de treino por id.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<WorkoutDto> HandleAsync(GetWorkoutByIdQuery query, CancellationToken cancellationToken)
    {
        Workout? workout = await repository.GetWorkoutAsync(query.WorkoutId, cancellationToken);
        
        if (workout is null)
            throw new NotFoundException($"Workout with id '{query.WorkoutId}' not found.");
        
        return new WorkoutDto(workout);
    }
}