using NutritionService.Common;
using NutritionService.UserFood;

namespace NutritionService.PublicFood.Common.Repository;

public interface IPublicFoodMongoRepository
{
    Task CreatePublicFoodAsync(Food food);
    Task<Food?> GetPublicFoodByIdAsync(string? id);
    Task<Food?> GetPublicFoodByBarCodeAsync(string? barCode);
    Task UpdatePublicFoodAsync(Food updatedFood);
    Task DeletePublicFoodAsync(string? id);
    Task<IEnumerable<Food>> ListUnrevisedPublicFoodsAsync(int page, int size);
    Task<IEnumerable<Food>> ListPublicFoodsAsync(int page, int size);
    Task<IEnumerable<Food>> ListPublicFoodsCreatedByUserAsync(int page, int size);
    Task<IEnumerable<Food>> ListPublicFoodsUsedByUserAsync(int page, int size,  string userId);
    Task<IEnumerable<Food>> ListRevisedPublicFoodsAsync(int page, int size);
    Task<bool> PublicFoodExistsAsync(string? id);
    Task<IEnumerable<Food>> GetManyByIdsAsync(string[] itemPublicFoodIds, CancellationToken cancellationToken);
}