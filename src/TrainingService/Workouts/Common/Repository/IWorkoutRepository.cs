namespace TrainingService.Workouts.Common.Repository;

public interface IWorkoutRepository
{
    Task<Workout?> GetWorkoutAsync(Guid workoutId, CancellationToken cancellationToken);
    
    Task<ICollection<Workout>> GetWorkoutsByUserIdAsync(string userId, CancellationToken cancellationToken);
    
    Task AddAsync(Workout workout, CancellationToken cancellationToken);
    
    Task DeleteAsync(Workout workout, CancellationToken cancellationToken);
    
    Task UpdateAsync(Workout workout, CancellationToken cancellationToken);
}