using MongoDB.Driver;
using NutritionService.Common;
using NutritionService.Connections;
using SharedKernel.Utils;

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

    public async Task InsertUserFoodAsync(Food food)
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
    
    public async Task<IEnumerable<Food>> ListFoodsAsync(int page, int size, string userId)
    {
        return await context.UserFoods.Find(f => f.UserId == userId)
            .Skip((page - 1) * size)
            .Limit(size)
            .ToListAsync();
    }

    public async Task<bool> UserFoodExistsAsync(string? id)
    {
        return await context.UserFoods.Find(f => f.Id == id).AnyAsync();
    }

    public async Task<IEnumerable<Food>> GetManyByIdsAsync(string[] foodIds, CancellationToken cancellationToken)
    {
        if (foodIds == null || foodIds.Length == 0)
        {
            return await Task.FromResult<IEnumerable<Food>>(Array.Empty<Food>());
        }

        var filter = Builders<Food>.Filter.In(f => f.Id, foodIds);
        return await context.UserFoods.Find(filter).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Food>> GetAllByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return await Task.FromResult<IEnumerable<Food>>(Array.Empty<Food>());
        }

        var filter = Builders<Food>.Filter.Eq(f => f.CreatedBy, userId);
        return await context.UserFoods.Find(filter).ToListAsync(cancellationToken);
    }

    public async Task InsertManyAsync(List<Food>? newFoods, CancellationToken cancellationToken)
    {
        if (newFoods == null || newFoods.Count == 0)
        {
            return;
        }

        await context.UserFoods.InsertManyAsync(newFoods, cancellationToken: cancellationToken);
    }
}