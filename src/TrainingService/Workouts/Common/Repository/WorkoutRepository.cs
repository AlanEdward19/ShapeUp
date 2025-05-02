using Microsoft.EntityFrameworkCore;
using SharedKernel.Exceptions;
using TrainingService.Connections.Database;
using TrainingService.Exercises.Common.Enums;

namespace TrainingService.Workouts.Common.Repository;

public class WorkoutRepository(TrainingDbContext dbContext) : IWorkoutRepository
{
    public async Task<Workout?> GetWorkoutAsync(Guid workoutId, CancellationToken cancellationToken)
    {
        Workout? workout = await dbContext.Workouts.AsNoTracking().Include(x => x.Exercises)
            .FirstOrDefaultAsync(x => x.Id == workoutId, cancellationToken);

        if (workout is null)
            throw new NotFoundException($"Workout with id '{workoutId}' not found.");

        return workout;
    }
    
    public async Task<ICollection<Workout>> GetWorkoutsByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        List<Workout> workouts = await dbContext.Workouts.AsNoTracking()
            .Where(x => x.UserId == userId)
            .Include(x => x.Exercises)
            .ToListAsync(cancellationToken);

        if (workouts is null || !workouts.Any())
            throw new NotFoundException($"No workouts for user id '{userId}' were found.");

        return workouts;
    }
    
    public async Task AddAsync(Workout workout, CancellationToken cancellationToken)
    {
        await dbContext.Database.BeginTransactionAsync(cancellationToken);

        await dbContext.Workouts.AddAsync(workout, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        await dbContext.Database.CommitTransactionAsync(cancellationToken);
    }
    
    public async Task UpdateAsync(Workout workout, CancellationToken cancellationToken)
    {
        await dbContext.Database.BeginTransactionAsync(cancellationToken);

        dbContext.Workouts.Update(workout);

        await dbContext.SaveChangesAsync(cancellationToken);

        await dbContext.Database.CommitTransactionAsync(cancellationToken);
    }
    
    public async Task DeleteAsync(Workout workout, CancellationToken cancellationToken)
    {
        await dbContext.Database.BeginTransactionAsync(cancellationToken);

        dbContext.Workouts.Remove(workout);

        await dbContext.SaveChangesAsync(cancellationToken);

        await dbContext.Database.CommitTransactionAsync(cancellationToken);
    }
}