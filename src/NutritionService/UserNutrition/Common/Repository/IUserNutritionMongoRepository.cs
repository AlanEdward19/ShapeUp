namespace NutritionService.UserNutrition.Common.Repository;

public interface IUserNutritionMongoRepository
{
    Task<UserNutrition?> GetUserNutritionDetailsAsync(string? userNutritionId);
    Task InsertUserNutritionAsync(UserNutrition userNutrition);
    Task UpdateUserNutritionAsync(UserNutrition updatedUserNutrition);
    Task DeleteUserNutritionAsync(string? userNutritionId);
    Task<List<UserNutrition>> ListUserNutritionAsync(string? userId);
}