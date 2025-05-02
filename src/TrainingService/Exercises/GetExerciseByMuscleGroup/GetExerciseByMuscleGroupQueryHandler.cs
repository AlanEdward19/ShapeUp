using TrainingService.Common.Interfaces;
using TrainingService.Exercises.Common;
using TrainingService.Exercises.Common.Repository;

namespace TrainingService.Exercises.GetExerciseByMuscleGroup;

public class GetExerciseByMuscleGroupQueryHandler(IExerciseRepository repository) 
    : IHandler<ICollection<ExerciseDto>, GetExerciseByMuscleGroupQuery>
{
    public async Task<ICollection<ExerciseDto>> HandleAsync(GetExerciseByMuscleGroupQuery query, CancellationToken cancellationToken)
    {
        ICollection<Exercise> exercises = await repository.GetExercisesByMuscleGroupAsync(query.MuscleGroup, cancellationToken);
        
        return exercises
            .Select(exercise => new ExerciseDto(exercise))
            .ToList();
    }
}