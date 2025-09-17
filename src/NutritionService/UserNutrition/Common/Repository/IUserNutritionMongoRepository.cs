namespace NutritionService.UserNutrition.Common.Repository;

public interface IUserNutritionMongoRepository
{
    Task<UserNutrition?> GetUserNutritionDetailsAsync(string? userNutritionId);
    Task InsertUserNutritionAsync(UserNutrition userNutrition);
    Task UpdateUserNutritionAsync(UserNutrition updatedUserNutrition);
    Task DeleteUserNutritionAsync(string? userNutritionId);
    Task<IEnumerable<UserNutrition>> ListManagedUserNutritionsAsync(int itemPage, int itemRows,
        CancellationToken cancellationToken, string nutritionManagerId);
}