using Microsoft.EntityFrameworkCore;
using TrainingService.Connections.Database;

namespace TrainingService.Workouts.Common.Repository;

public class WorkoutRepository(TrainingDbContext dbContext) : IWorkoutRepository
{
    public async Task<Workout?> GetWorkoutAsync(Guid workoutId, CancellationToken cancellationToken,bool track = false)
    {
        IQueryable<Workout> query = dbContext
            .Workouts
            .Include(x => x.Exercises);
        
        if(!track)
            query = query.AsNoTracking();

        return await query
            .FirstOrDefaultAsync(x => x.Id == workoutId, cancellationToken);
    }
    
    public async Task<ICollection<Workout>> GetWorkoutsByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        List<Workout> workouts = await dbContext.Workouts.AsNoTracking()
            .Where(x => x.UserId == userId)
            .Include(x => x.Exercises)
            .ToListAsync(cancellationToken);

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