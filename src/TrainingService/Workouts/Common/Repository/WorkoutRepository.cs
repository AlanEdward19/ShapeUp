using Microsoft.EntityFrameworkCore;
using TrainingService.Connections.Database;

namespace TrainingService.Workouts.Common.Repository;

/// <summary>
/// Repositório para gerenciar operações relacionadas a treinos no banco de dados.
/// </summary>
/// <param name="dbContext"></param>
public class WorkoutRepository(TrainingDbContext dbContext) : IWorkoutRepository
{
    /// <summary>
    /// Método para verificar se um treino existe pelo ID.
    /// </summary>
    /// <param name="workoutId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> WorkoutExistsAsync(Guid workoutId, CancellationToken cancellationToken)
    {
        return await dbContext.Workouts
            .AsNoTracking()
            .AnyAsync(x => x.Id == workoutId, cancellationToken);
    }

    /// <summary>
    /// Método para obter um treino por ID.
    /// </summary>
    /// <param name="workoutId"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="track"></param>
    /// <returns></returns>
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
    
    /// <summary>
    /// Método para obter treinos pelo ID do usuário.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ICollection<Workout>> GetWorkoutsByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        List<Workout> workouts = await dbContext.Workouts.AsNoTracking()
            .Where(x => x.UserId == userId)
            .Include(x => x.Exercises)
            .ToListAsync(cancellationToken);

        return workouts;
    }
    
    /// <summary>
    /// Método para adicionar um novo treino.
    /// </summary>
    /// <param name="workout"></param>
    /// <param name="cancellationToken"></param>
    public async Task AddAsync(Workout workout, CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.Database.BeginTransactionAsync(cancellationToken);

            await dbContext.Workouts.AddAsync(workout, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            await dbContext.Database.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception e)
        {
            await dbContext.Database.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
    
    /// <summary>
    /// Método para deletar um treino existente.
    /// </summary>
    /// <param name="workout"></param>
    /// <param name="cancellationToken"></param>
    public async Task UpdateAsync(Workout workout, CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.Database.BeginTransactionAsync(cancellationToken);

            dbContext.Workouts.Update(workout);

            await dbContext.SaveChangesAsync(cancellationToken);

            await dbContext.Database.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception e)
        {
            await dbContext.Database.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
    
    /// <summary>
    /// Método para atualizar um treino existente.
    /// </summary>
    /// <param name="workout"></param>
    /// <param name="cancellationToken"></param>
    public async Task DeleteAsync(Workout workout, CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.Database.BeginTransactionAsync(cancellationToken);

            dbContext.Workouts.Remove(workout);

            await dbContext.SaveChangesAsync(cancellationToken);

            await dbContext.Database.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception e)
        {
            await dbContext.Database.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}