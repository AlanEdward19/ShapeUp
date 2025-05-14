using MongoDB.Driver;
using NutritionService.Connections;

namespace NutritionService.UserFood.Common.Repository;

public class UserFoodMongoRepository(NutritionDbContext context) : IUserFoodMongoRepository
{
    public async Task<Food?> GetUserFoodByIdAsync(string? id)
    {
        if (string.IsNullOrWhiteSpace(id)) return null;
        return await context.UserFoods.Find(food => food.Id == id).SingleOrDefaultAsync();
    }
    
    public async Task<Food?>GetUserFoodByBarCodeAsync(string? barCode)
    {
        if (string.IsNullOrWhiteSpace(barCode)) return null;
        return await context.UserFoods.Find(food => food.BarCode == barCode).SingleOrDefaultAsync();
    }

    public async Task CreateUserFoodAsync(Food food)
    {
        ArgumentNullException.ThrowIfNull(food);
        await context.UserFoods.InsertOneAsync(food);
    }

    public async Task UpdateUserFoodAsync(Food updatedFood)
    {
        ArgumentNullException.ThrowIfNull(updatedFood);
        var filter = Builders<Food>.Filter.Eq(nameof(Food.Id), updatedFood.Id);
        await context.UserFoods.ReplaceOneAsync(filter, updatedFood);
    }

    public async Task DeleteUserFoodAsync(string? id)
    {
        if (string.IsNullOrWhiteSpace(id)) return;
        var filter = Builders<Food>.Filter.Eq(nameof(Food.Id), id);
        await context.UserFoods.DeleteOneAsync(filter);
    }
    
    public async Task<IEnumerable<Food>> ListUnrevisedFoodsAsync()
    {
        return await context.UserFoods.Find(food => !food.Revised).ToListAsync();
    }
}