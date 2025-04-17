using MongoDB.Driver;
using NutritionService.Connections;

namespace NutritionService.Dish.Common.Repository;

/// <summary>
/// 
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
}