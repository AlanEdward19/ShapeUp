using MongoDB.Driver;

namespace NutritionService.Dish.Common.Repository;

/// <summary>
/// 
/// </summary>
public class DishMongoRepository(IMongoDatabase database) : IDishMongoRepository
{
    private readonly IMongoCollection<Dish> _dishCollection = database.GetCollection<Dish>("dishes");

    public async Task<Dish?> GetDishByIdAsync(string? id)
    {
        if (string.IsNullOrWhiteSpace(id)) return null;
        return await _dishCollection.Find(d => d.Id == id).SingleOrDefaultAsync();
    }

    public async Task<Dish?> GetDishByBarCodeAsync(string? barCode)
    {
        if (string.IsNullOrWhiteSpace(barCode)) return null;
        return await _dishCollection.Find(d => d.BarCode == barCode).SingleOrDefaultAsync();
    }

    public async Task InsertDishAsync(Dish dish)
    {
        await _dishCollection.InsertOneAsync(dish);
    }

    public async Task UpdateDishAsync(Dish updatedDish)
    {
        var filter = Builders<Dish>.Filter.Eq(d => d.Id, updatedDish.Id);
        await _dishCollection.ReplaceOneAsync(filter, updatedDish);
    }

    public async Task DeleteDishAsync(string? id)
    {
        await _dishCollection.DeleteOneAsync(d => d.Id == id);
    }
}