using MongoDB.Driver;

namespace NutritionService.Food.Common.Repository;

public class FoodMongoRepository(IMongoDatabase database) : IFoodMongoRepository
{
    private readonly IMongoCollection<Food> _foodCollection = database.GetCollection<Food>("foods");

    public async Task<Food>? GetFoodByIdAsync(string id)
    {
        return await database.GetCollection<Food>("foods").Find(f => f.Id == id).SingleOrDefaultAsync();
    }

    public async Task<Food?> GetFoodByBarCodeAsync(string? barCode)
    {
        if (string.IsNullOrWhiteSpace(barCode)) return null;
        return await _foodCollection.Find(food => food.BarCode == barCode).SingleOrDefaultAsync();
    }

    public async Task InsertFoodAsync(Food food)
    {
        await _foodCollection.InsertOneAsync(food);
    }

    public async Task UpdateFoodAsync(Food updatedFood)
    {
        var filter = Builders<Food>.Filter.Eq(nameof(Food.BarCode), updatedFood.BarCode);
        await _foodCollection.ReplaceOneAsync(filter, updatedFood);
    }

    public async Task DeleteFoodAsync(string? barCode)
    {
        var filter = Builders<Food>.Filter.Eq(nameof(Food.BarCode), barCode);
        await _foodCollection.DeleteOneAsync(filter);
    }
    
    public async Task<IEnumerable<Food>> ListUnrevisedFoodsAsync()
    {
        return await _foodCollection.Find(food => !food.Revised).ToListAsync();
    }
}