using TrainingService.Common.Interfaces;
using TrainingService.Exercises.Common;
using TrainingService.Exercises.Common.Repository;

namespace TrainingService.Exercises.GetExerciseById;

public class GetExerciseByIdQueryHandler(IExerciseRepository repository) 
    : IHandler<ExerciseDto, GetExerciseByIdQuery>
{
    public async Task<ExerciseDto> HandleAsync(GetExerciseByIdQuery query, CancellationToken cancellationToken)
    {
        Exercise? exercise = await repository.GetExerciseByIdAsync(query.ExerciseId, cancellationToken);
        ArgumentNullException.ThrowIfNull(exercise, nameof(exercise));
        
        return new ExerciseDto(exercise);
    }
}