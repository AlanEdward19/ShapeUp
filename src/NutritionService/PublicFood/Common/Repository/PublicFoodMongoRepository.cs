using MongoDB.Driver;
using NutritionService.Common;
using NutritionService.Connections;
using NutritionService.UserFood;

namespace NutritionService.PublicFood.Common.Repository;

public class PublicFoodMongoRepository(NutritionDbContext context) : IPublicFoodMongoRepository
{
    public async Task CreatePublicFoodAsync(Food food)
    {
        ArgumentNullException.ThrowIfNull(food);
        await context.PublicFoods.InsertOneAsync(food);
    }

    public async Task<Food?> GetPublicFoodByIdAsync(string? id)
    {
        if (string.IsNullOrWhiteSpace(id)) return null;
        return await context.PublicFoods.Find(food => food.Id == id).SingleOrDefaultAsync();
    }

    public async Task<Food?> GetPublicFoodByBarCodeAsync(string? barCode)
    {
        if (string.IsNullOrWhiteSpace(barCode)) return null;
        return await context.PublicFoods.Find(food => food.BarCode == barCode).SingleOrDefaultAsync();
    }

    public async Task UpdatePublicFoodAsync(Food updatedFood)
    {
        ArgumentNullException.ThrowIfNull(updatedFood);
        var filter = Builders<Food>.Filter.Eq(nameof(Food.Id), updatedFood.Id);
        await context.PublicFoods.ReplaceOneAsync(filter, updatedFood);
    }

    public Task DeletePublicFoodAsync(string? id)
    {
        if (string.IsNullOrWhiteSpace(id)) return Task.CompletedTask;
        var filter = Builders<Food>.Filter.Eq(nameof(Food.Id), id);
        return context.PublicFoods.DeleteOneAsync(filter);
    }

    public async Task<IEnumerable<Food>> ListUnrevisedPublicFoodsAsync(int page, int size)
    {
        return await context.PublicFoods.Find(food => food.IsRevised == false)
            .Skip((page - 1) * size)
            .Limit(size)
            .ToListAsync();
    }

    public async Task<IEnumerable<Food>> ListPublicFoodsAsync(int page, int size)
    {
        return await context.PublicFoods.Find(_ => true)
            .Skip((page - 1) * size)
            .Limit(size)
            .ToListAsync();
    }

    public async Task<IEnumerable<Food>> ListRevisedPublicFoodsAsync(int page, int size)
    {
        return await context.PublicFoods.Find(food => food.IsRevised == true)
            .Skip((page - 1) * size)
            .Limit(size)
            .ToListAsync();
    }

    public async Task<bool> PublicFoodExistsAsync(string? id)
    {
        return await context.PublicFoods.Find(f => f.Id == id).AnyAsync();
    }

    public async Task<IEnumerable<Food>> GetManyByIdsAsync(string[] itemPublicFoodIds, CancellationToken cancellationToken)
    {
        if (itemPublicFoodIds == null || itemPublicFoodIds.Length == 0)
            return Enumerable.Empty<Food>();

        var filter = Builders<Food>.Filter.In(f => f.Id, itemPublicFoodIds);
        return await context.PublicFoods.Find(filter).ToListAsync(cancellationToken);
    }
}