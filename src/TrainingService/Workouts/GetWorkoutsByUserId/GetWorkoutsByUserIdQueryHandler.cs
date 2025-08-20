using TrainingService.Common.Interfaces;
using TrainingService.Workouts.Common;
using TrainingService.Workouts.Common.Repository;

namespace TrainingService.Workouts.GetWorkoutsByUserId;

/// <summary>
/// Handler para a query de obtenção de treinos por id do usuário.
/// </summary>
/// <param name="repository"></param>
public class GetWorkoutsByUserIdQueryHandler(IWorkoutRepository repository) 
    : IHandler<ICollection<WorkoutDto>, GetWorkoutsByUserIdQuery>
{
    /// <summary>
    /// Método para tratar a query de obtenção de treinos por id do usuário.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ICollection<WorkoutDto>> HandleAsync(GetWorkoutsByUserIdQuery query, CancellationToken cancellationToken)
    {
        ICollection<Workout> workouts = await repository.GetWorkoutsByUserIdAsync(query.UserId, cancellationToken);
        
        return workouts
            .Select(workout => new WorkoutDto(workout))
            .ToList();
    }
}