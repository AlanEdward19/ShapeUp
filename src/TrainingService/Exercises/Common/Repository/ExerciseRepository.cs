using Microsoft.EntityFrameworkCore;
using SharedKernel.Exceptions;
using TrainingService.Connections.Database;
using TrainingService.Exercises.Common.Enums;

namespace TrainingService.Exercises.Common.Repository;

/// <summary>
/// Repositório para gerenciar operações relacionadas a exercícios no banco de dados.
/// </summary>
/// <param name="dbContext"></param>
public class ExerciseRepository(TrainingDbContext dbContext) : IExerciseRepository
{
    /// <summary>
    /// Método para verificar se um exercício existe pelo ID.
    /// </summary>
    /// <param name="exerciseId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> ExerciseExistsAsync(Guid exerciseId, CancellationToken cancellationToken)
    {
        return await dbContext.Exercises
            .AsNoTracking()
            .AnyAsync(x => x.Id == exerciseId, cancellationToken);
    }

    /// <summary>
    /// Método para verificar se uma coleção de exercícios existe pelo ID.
    /// </summary>
    /// <param name="exerciseId"></param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="NotFoundException"></exception>
    public async Task ExerciseExistsAsync(ICollection<Guid> exerciseId, CancellationToken cancellationToken)
    {
        if (exerciseId is null || !exerciseId.Any())
            return;

        List<Guid> existingIds = await dbContext.Exercises
            .AsNoTracking()
            .Where(x => exerciseId.Contains(x.Id))
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);

        List<Guid> notFoundIds = exerciseId
            .Where(id => !existingIds.Contains(id))
            .ToList();

        if (notFoundIds.Any())
            throw new NotFoundException($"Exercises with IDs '{string.Join(", ", notFoundIds)}' not found.");
    }

    /// <summary>
    /// Método para obter um exercício por ID.
    /// </summary>
    /// <param name="exerciseId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<Exercise> GetExerciseByIdAsync(Guid exerciseId, CancellationToken cancellationToken)
    {
        Exercise? exercise = await dbContext.Exercises
            .FirstOrDefaultAsync(x => x.Id == exerciseId, cancellationToken);

        if (exercise is null)
            throw new NotFoundException($"Exercise with id '{exerciseId}' not found.");

        return exercise;
    }

    /// <summary>
    /// Método para obter exercícios por uma lista de IDs.
    /// </summary>
    /// <param name="exerciseIds"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="track"></param>
    /// <returns></returns>
    public async Task<ICollection<Exercise>> GetExercisesByIdsAsync(List<Guid> exerciseIds,
        CancellationToken cancellationToken, bool track = false)
    {
        if (exerciseIds is null || !exerciseIds.Any())
            return [];

        var exerciseQueryable = dbContext.Exercises.AsQueryable();

        if (!track)
            exerciseQueryable = exerciseQueryable.AsNoTracking();

        exerciseQueryable = exerciseQueryable.Where(x => exerciseIds.Contains(x.Id));

        return await exerciseQueryable
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Método para obter exercícios por grupo muscular.
    /// </summary>
    /// <param name="muscleGroup"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<ICollection<Exercise>> GetExercisesByMuscleGroupAsync(EMuscleGroup muscleGroup,
        CancellationToken cancellationToken)
    {
        List<Exercise> exercises = await dbContext.Exercises.AsNoTracking()
            .Where(x => (x.MuscleGroups & muscleGroup) != 0)
            .ToListAsync(cancellationToken);

        if (exercises is null || !exercises.Any())
            throw new NotFoundException($"No exercises were found for the muscle group '{muscleGroup}'.");

        return exercises;
    }

    /// <summary>
    /// Método para obter todos os exercícios.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ICollection<Exercise>> GetExercisesAsync(CancellationToken cancellationToken)
    {
        List<Exercise> exercises = await dbContext.Exercises.AsNoTracking()
            .ToListAsync(cancellationToken);

        return exercises;
    }

    /// <summary>
    /// Método para adicionar um exercício.
    /// </summary>
    /// <param name="exercise"></param>
    /// <param name="cancellationToken"></param>
    public async Task AddAsync(Exercise exercise, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            try
            {
                await dbContext.Database.BeginTransactionAsync(cancellationToken);

                await dbContext.Exercises.AddAsync(exercise, cancellationToken);

                await dbContext.SaveChangesAsync(cancellationToken);

                await dbContext.Database.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception e)
            {
                await dbContext.Database.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        });
    }

    /// <summary>
    /// Método para atualizar um exercício.
    /// </summary>
    /// <param name="exercise"></param>
    /// <param name="cancellationToken"></param>
    public async Task UpdateAsync(Exercise exercise, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            try
            {
                await dbContext.Database.BeginTransactionAsync(cancellationToken);

                dbContext.Exercises.Update(exercise);

                await dbContext.SaveChangesAsync(cancellationToken);

                await dbContext.Database.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception e)
            {
                await dbContext.Database.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        });
    }

    /// <summary>
    /// Método para deletar um exercício.
    /// </summary>
    /// <param name="exercise"></param>
    /// <param name="cancellationToken"></param>
    public async Task DeleteAsync(Exercise exercise, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            try
            {
                await dbContext.Database.BeginTransactionAsync(cancellationToken);

                dbContext.Exercises.Remove(exercise);

                await dbContext.SaveChangesAsync(cancellationToken);

                await dbContext.Database.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception e)
            {
                await dbContext.Database.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        });
    }
}