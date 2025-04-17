using MongoDB.Driver;
using NutritionService.Connections;

namespace NutritionService.Meal.Common;

public class MealMongoRepository(NutritionDbContext context) : IMealMongoRepository
{
    public async Task<Meal?> GetMealByIdAsync(string? id)
    {
        if (string.IsNullOrWhiteSpace(id)) return null;
        return await context.Meals.Find(meal => meal.Id == id).SingleOrDefaultAsync();
    }

    public async Task InsertMealAsync(Meal meal)
    {
        await context.Meals.InsertOneAsync(meal);
    }

    public Task UpdateMealAsync(Meal updatedMeal)
    {
        var filter = Builders<Meal>.Filter.Eq(m => m.Id, updatedMeal.Id);
        return context.Meals.ReplaceOneAsync(filter, updatedMeal);
    }

    public Task DeleteMealAsync(string? id)
    {
        var filter = Builders<Meal>.Filter.Eq(m => m.Id, id);
        return context.Meals.DeleteOneAsync(filter);
    }
}