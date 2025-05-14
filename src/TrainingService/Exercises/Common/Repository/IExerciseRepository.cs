using TrainingService.Exercises.Common.Enums;

namespace TrainingService.Exercises.Common.Repository;

public interface IExerciseRepository
{
    Task<Exercise?> GetExerciseAsync(Guid exerciseId, CancellationToken cancellationToken);
    
    Task<ICollection<Exercise>> GetExercisesByMuscleGroupAsync(EMuscleGroup muscleGroup, CancellationToken cancellationToken);
    
    Task AddAsync(Exercise exercise, CancellationToken cancellationToken);
    
    Task DeleteAsync(Exercise exercise, CancellationToken cancellationToken);
    
    Task UpdateAsync(Exercise exercise, CancellationToken cancellationToken);
}