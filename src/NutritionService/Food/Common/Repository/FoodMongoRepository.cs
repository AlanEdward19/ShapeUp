using MongoDB.Driver;

namespace NutritionService.Food.Common.Repository;

public class FoodMongoRepository(IMongoDatabase database) : IFoodMongoRepository
{
    private readonly IMongoCollection<Food> _foodCollection = database.GetCollection<Food>("Foods");

    public async Task<Food?> GetFoodByNameAsync(string name)
    {
        return await _foodCollection.Find(food => food.Name == name).FirstOrDefaultAsync();
    }

    public async Task<Food?> GetFoodByBarCodeAsync(string? barCode)
    {
        return await _foodCollection.Find(food => food.BarCode == barCode).FirstOrDefaultAsync();
    }

    public async Task InsertFoodAsync(Food food)
    {
        await _foodCollection.InsertOneAsync(food);
    }

    public async Task UpdateFoodAsync(Food updatedFood)
    {
        var filter = Builders<Food>.Filter.Eq("barCode", updatedFood.BarCode);
        await _foodCollection.ReplaceOneAsync(filter, updatedFood);
    }

    public async Task DeleteFoodAsync(string? barCode)
    {
        var filter = Builders<Food>.Filter.Eq("barCode", barCode);
        await _foodCollection.DeleteOneAsync(filter);
    }
    
    public async Task<IEnumerable<Food>> ListUnrevisedFoodsAsync()
    {
        return await _foodCollection.Find(food => !food.Revised).ToListAsync();
    }
}