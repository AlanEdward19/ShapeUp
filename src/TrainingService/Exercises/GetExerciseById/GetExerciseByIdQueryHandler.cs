using TrainingService.Common.Interfaces;
using TrainingService.Exercises.Common;
using TrainingService.Exercises.Common.Repository;

namespace TrainingService.Exercises.GetExerciseById;

/// <summary>
/// Handler para obter um exercício pelo ID
/// </summary>
/// <param name="repository"></param>
public class GetExerciseByIdQueryHandler(IExerciseRepository repository) 
    : IHandler<ExerciseDto, GetExerciseByIdQuery>
{
    /// <summary>
    /// Método para obter um exercício pelo ID.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ExerciseDto> HandleAsync(GetExerciseByIdQuery query, CancellationToken cancellationToken)
    {
        Exercise exercise = await repository.GetExerciseByIdAsync(query.ExerciseId, cancellationToken);
        
        return new ExerciseDto(exercise);
    }
}