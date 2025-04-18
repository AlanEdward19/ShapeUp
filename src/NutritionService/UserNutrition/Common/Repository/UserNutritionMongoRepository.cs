using MongoDB.Driver;
using NutritionService.Connections;

namespace NutritionService.UserNutrition.Common.Repository;

public class UserNutritionMongoRepository(NutritionDbContext context) : IUserNutritionMongoRepository
{
    public async Task<UserNutrition?> GetUserNutritionDetailsAsync(string? userNutritionId)
    {
        if (string.IsNullOrEmpty(userNutritionId)) 
            return null;
        var userNutrition = await context.UserNutritions.Find(x => x.Id == userNutritionId).FirstOrDefaultAsync();
        return userNutrition;
    }

    public async Task InsertUserNutritionAsync(UserNutrition userNutrition)
    {
        ArgumentNullException.ThrowIfNull(userNutrition);
        await context.UserNutritions.InsertOneAsync(userNutrition);
    }

    public async Task UpdateUserNutritionAsync(UserNutrition updatedUserNutrition)
    {
        ArgumentNullException.ThrowIfNull(updatedUserNutrition);
        await context.UserNutritions.ReplaceOneAsync(x => x.Id == updatedUserNutrition.Id, updatedUserNutrition);
    }

    public async Task DeleteUserNutritionAsync(string? userNutritionId)
    {
        if (string.IsNullOrEmpty(userNutritionId)) 
            return;
        var filter = Builders<UserNutrition>.Filter.Eq(x => x.Id, userNutritionId);
        await context.UserNutritions.DeleteOneAsync(filter);
    }

    public Task<List<UserNutrition>> ListUserNutritionAsync(string? userId)
    {
        throw new NotImplementedException();
    }
}