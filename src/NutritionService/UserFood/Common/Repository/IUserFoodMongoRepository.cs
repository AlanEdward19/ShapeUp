using NutritionService.Common;

namespace NutritionService.UserFood.Common.Repository;

public interface IUserFoodMongoRepository
{
    Task<Food?> GetUserFoodByIdAsync(string? id);
    Task<Food?> GetUserFoodByBarCodeAsync(string? barCode);
    Task InsertUserFoodAsync(Food food);
    Task UpdateUserFoodAsync(Food updatedFood);
    Task DeleteUserFoodAsync(string? id);
    Task<IEnumerable<Food>> ListFoodsAsync(int page, int size, string userId);
    Task<bool> UserFoodExistsAsync(string? id);
    Task<IEnumerable<Food>> GetManyByIdsAsync(string[] foodIds);
    Task<IEnumerable<Food>> GetAllByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task InsertManyAsync(List<Food>? newFoods, CancellationToken cancellationToken);
}