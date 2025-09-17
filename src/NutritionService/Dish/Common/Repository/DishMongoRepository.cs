using MongoDB.Driver;
using NutritionService.Connections;
using SharedKernel.Utils;

namespace NutritionService.Dish.Common.Repository;

/// <summary>
/// Repositório para gerenciar operações de CRUD para pratos no MongoDB.
/// </summary>
public class DishMongoRepository(NutritionDbContext context) : IDishMongoRepository
{
    public async Task<Dish?> GetDishByIdAsync(string? id)
    {
        if (string.IsNullOrWhiteSpace(id)) return null;
        return await context.Dishes.Find(d => d.Id == id).SingleOrDefaultAsync();
    }

    public async Task InsertDishAsync(Dish dish)
    {
        await context.Dishes.InsertOneAsync(dish);
    }

    public async Task UpdateDishAsync(Dish updatedDish)
    {
        var filter = Builders<Dish>.Filter.Eq(d => d.Id, updatedDish.Id);
        await context.Dishes.ReplaceOneAsync(filter, updatedDish);
    }

    public async Task DeleteDishAsync(string? id)
    {
        await context.Dishes.DeleteOneAsync(d => d.Id == id);
    }

    public async Task<IEnumerable<Dish>> GetManyByIdsAsync(string[] dishIds, CancellationToken cancellationToken)
    {
        if (dishIds == null || dishIds.Length == 0) return Enumerable.Empty<Dish>();

        var filter = Builders<Dish>.Filter.In(d => d.Id, dishIds);
        return await context.Dishes.Find(filter).ToListAsync(cancellationToken);
    }

    public Task<IEnumerable<Dish>> ListDihesAsync(int itemPage, int itemRows, CancellationToken cancellationToken,
        string userId)
    {
        return context.Dishes.Find(d => d.UserId == userId)
            .Skip(itemPage * itemRows)
            .Limit(itemRows)
            .ToListAsync(cancellationToken)
            .ContinueWith(task => task.Result.AsEnumerable(), cancellationToken);
    }
}