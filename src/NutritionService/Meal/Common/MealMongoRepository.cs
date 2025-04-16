using MongoDB.Driver;

namespace NutritionService.Meal.Common;

public class MealMongoRepository(IMongoDatabase database) : IMealMongoRepository
{
    private readonly IMongoCollection<Meal> _mealCollection = database.GetCollection<Meal>("meals");
    public async Task<Meal?> GetMealByIdAsync(string? id)
    {
        if (string.IsNullOrWhiteSpace(id)) return null;
        return await _mealCollection.Find(meal => meal.Id == id).SingleOrDefaultAsync();
    }

    public async Task InsertMealAsync(Meal meal)
    {
        await _mealCollection.InsertOneAsync(meal);
    }

    public Task UpdateMealAsync(Meal updatedMeal)
    {
        var filter = Builders<Meal>.Filter.Eq(m => m.Id, updatedMeal.Id);
        return _mealCollection.ReplaceOneAsync(filter, updatedMeal);
    }

    public Task DeleteMealAsync(string? id)
    {
        var filter = Builders<Meal>.Filter.Eq(m => m.Id, id);
        return _mealCollection.DeleteOneAsync(filter);
    }
}