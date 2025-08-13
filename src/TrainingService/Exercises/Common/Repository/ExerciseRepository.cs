using Microsoft.EntityFrameworkCore;
using SharedKernel.Exceptions;
using TrainingService.Connections.Database;
using TrainingService.Exercises.Common.Enums;

namespace TrainingService.Exercises.Common.Repository;

public class ExerciseRepository(TrainingDbContext dbContext) : IExerciseRepository
{
    public async Task<Exercise?> GetExerciseByIdAsync(Guid exerciseId, CancellationToken cancellationToken)
    {
        Exercise? exercise = await dbContext.Exercises
            .FirstOrDefaultAsync(x => x.Id == exerciseId, cancellationToken);

        if (exercise is null)
            throw new NotFoundException($"Exercise with id '{exerciseId}' not found.");

        return exercise;
    }

    public async Task<ICollection<Exercise>> GetExercisesByIdsAsync(List<Guid> exerciseIds, CancellationToken cancellationToken)
    {
        if (exerciseIds is null || !exerciseIds.Any())
            throw new ArgumentException("Exercise IDs cannot be null or empty.", nameof(exerciseIds));

        return await dbContext.Exercises.AsNoTracking()
            .Where(x => exerciseIds.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<ICollection<Exercise>> GetExercisesByMuscleGroupAsync(EMuscleGroup muscleGroup, CancellationToken cancellationToken)
    {
        List<Exercise> exercises = await dbContext.Exercises.AsNoTracking()
            .Where(x => (x.MuscleGroups & muscleGroup) != 0)
            .ToListAsync(cancellationToken);

        if (exercises is null || !exercises.Any())
            throw new NotFoundException($"No exercises were found for the muscle group '{muscleGroup}'.");

        return exercises;
    }

    public async Task<ICollection<Exercise>> GetExercisesAsync(CancellationToken cancellationToken)
    {
        List<Exercise> exercises = await dbContext.Exercises.AsNoTracking()
            .ToListAsync(cancellationToken);

        return exercises;
    }

    public async Task AddAsync(Exercise exercise, CancellationToken cancellationToken)
    {
        await dbContext.Database.BeginTransactionAsync(cancellationToken);

        await dbContext.Exercises.AddAsync(exercise, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        await dbContext.Database.CommitTransactionAsync(cancellationToken);
    }
    
    public async Task UpdateAsync(Exercise exercise, CancellationToken cancellationToken)
    {
        await dbContext.Database.BeginTransactionAsync(cancellationToken);

        dbContext.Exercises.Update(exercise);

        await dbContext.SaveChangesAsync(cancellationToken);

        await dbContext.Database.CommitTransactionAsync(cancellationToken);
    }
    
    public async Task DeleteAsync(Exercise exercise, CancellationToken cancellationToken)
    {
        await dbContext.Database.BeginTransactionAsync(cancellationToken);

        dbContext.Exercises.Remove(exercise);

        await dbContext.SaveChangesAsync(cancellationToken);

        await dbContext.Database.CommitTransactionAsync(cancellationToken);
    }
}