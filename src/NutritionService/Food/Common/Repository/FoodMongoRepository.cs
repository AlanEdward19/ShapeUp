using MongoDB.Driver;
using NutritionService.Connections;

namespace NutritionService.Food.Common.Repository;

public class FoodMongoRepository(NutritionDbContext context) : IFoodMongoRepository
{
    public async Task<Food?> GetFoodByIdAsync(string? id)
    {
        if (string.IsNullOrWhiteSpace(id)) return null;
        return await context.Foods.Find(food => food.Id == id).SingleOrDefaultAsync();
    }

    public async Task InsertFoodAsync(Food food)
    {
        ArgumentNullException.ThrowIfNull(food);
        await context.Foods.InsertOneAsync(food);
    }

    public async Task UpdateFoodAsync(Food updatedFood)
    {
        ArgumentNullException.ThrowIfNull(updatedFood);
        var filter = Builders<Food>.Filter.Eq(nameof(Food.Id), updatedFood.Id);
        await context.Foods.ReplaceOneAsync(filter, updatedFood);
    }

    public async Task DeleteFoodAsync(string? id)
    {
        if (string.IsNullOrWhiteSpace(id)) return;
        var filter = Builders<Food>.Filter.Eq(nameof(Food.Id), id);
        await context.Foods.DeleteOneAsync(filter);
    }
    
    public async Task<IEnumerable<Food>> ListUnrevisedFoodsAsync()
    {
        return await context.Foods.Find(food => !food.Revised).ToListAsync();
    }
}