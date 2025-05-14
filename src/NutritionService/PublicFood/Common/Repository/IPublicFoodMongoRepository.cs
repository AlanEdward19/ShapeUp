using NutritionService.UserFood;

namespace NutritionService.PublicFood.Common.Repository;

public interface IPublicFoodMongoRepository
{
    Task CreatePublicFoodAsync(Food food);
    Task<Food?> GetPublicFoodByIdAsync(string? id);
    Task<Food?> GetPublicFoodByBarCodeAsync(string? barCode);
    Task UpdatePublicFoodAsync(Food updatedFood);
    Task DeletePublicFoodAsync(string? id);
    Task<IEnumerable<Food>> ListUnrevisedPublicFoodsAsync();
    Task<IEnumerable<Food>> ListPublicFoodsAsync();
}