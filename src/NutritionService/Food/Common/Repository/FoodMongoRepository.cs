using MongoDB.Driver;

namespace NutritionService.Food.Common.Repository;

public class FoodMongoRepository(IMongoDatabase database) : IFoodMongoRepository
{
    private readonly IMongoCollection<Food> _foodCollection = database.GetCollection<Food>("foods");
    

    public async Task<Food?> GetFoodByIdAsync(string? id)
    {
        if (string.IsNullOrWhiteSpace(id)) return null;
        return await _foodCollection.Find(food => food.Id == id).SingleOrDefaultAsync();
    }

    public async Task InsertFoodAsync(Food food)
    {
        await _foodCollection.InsertOneAsync(food);
    }

    public async Task UpdateFoodAsync(Food updatedFood)
    {
        var filter = Builders<Food>.Filter.Eq(nameof(Food.Id), updatedFood.Id);
        await _foodCollection.ReplaceOneAsync(filter, updatedFood);
    }

    public async Task DeleteFoodAsync(string? id)
    {
        var filter = Builders<Food>.Filter.Eq(nameof(Food.Id), id);
        await _foodCollection.DeleteOneAsync(filter);
    }
    
    public async Task<IEnumerable<Food>> ListUnrevisedFoodsAsync()
    {
        return await _foodCollection.Find(food => !food.Revised).ToListAsync();
    }
}