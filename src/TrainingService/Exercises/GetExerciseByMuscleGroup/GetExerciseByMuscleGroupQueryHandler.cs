using TrainingService.Common.Interfaces;
using TrainingService.Exercises.Common;
using TrainingService.Exercises.Common.Repository;

namespace TrainingService.Exercises.GetExerciseByMuscleGroup;

/// <summary>
/// Handler para obter exercícios por grupo muscular.
/// </summary>
/// <param name="repository"></param>
public class GetExerciseByMuscleGroupQueryHandler(IExerciseRepository repository)
    : IHandler<ICollection<ExerciseDto>, GetExerciseByMuscleGroupQuery>
{
    /// <summary>
    /// Método para obter exercícios por grupo muscular.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ICollection<ExerciseDto>> HandleAsync(GetExerciseByMuscleGroupQuery query,
        CancellationToken cancellationToken)
    {
        ICollection<Exercise> exercises = query.MuscleGroup != null
            ? await repository.GetExercisesByMuscleGroupAsync(query.MuscleGroup.Value, cancellationToken)
            : await repository.GetExercisesAsync(cancellationToken);

        return exercises
            .Select(exercise => new ExerciseDto(exercise))
            .ToList();
    }
}