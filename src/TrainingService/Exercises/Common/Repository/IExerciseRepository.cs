using TrainingService.Exercises.Common.Enums;

namespace TrainingService.Exercises.Common.Repository;

public interface IExerciseRepository
{
    Task<Exercise?> GetExerciseByIdAsync(Guid exerciseId, CancellationToken cancellationToken);

    Task<ICollection<Exercise>> GetExercisesByIdsAsync(List<Guid> exerciseIds, CancellationToken cancellationToken,
        bool track = false);
    
    Task<ICollection<Exercise>> GetExercisesByMuscleGroupAsync(EMuscleGroup muscleGroup, CancellationToken cancellationToken);
    
    Task<ICollection<Exercise>> GetExercisesAsync(CancellationToken cancellationToken);
    
    Task AddAsync(Exercise exercise, CancellationToken cancellationToken);
    
    Task DeleteAsync(Exercise exercise, CancellationToken cancellationToken);
    
    Task UpdateAsync(Exercise exercise, CancellationToken cancellationToken);
}