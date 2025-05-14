namespace NutritionService.UserFood.Common.Repository;

public interface IUserFoodMongoRepository
{
    Task<Food?> GetUserFoodByIdAsync(string? id);
    Task<Food?> GetUserFoodByBarCodeAsync(string? barCode);
    Task CreateUserFoodAsync(Food food);
    Task UpdateUserFoodAsync(Food updatedFood);
    Task DeleteUserFoodAsync(string? barCode);
    Task<IEnumerable<Food>> ListUnrevisedFoodsAsync();
}