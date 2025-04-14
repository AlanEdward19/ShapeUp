namespace NutritionService.Food.Common.Repository;

public interface IFoodMongoRepository
{
    Task<Food>? GetFoodByIdAsync(string id);
    Task<Food?> GetFoodByBarCodeAsync(string? barCode);
    Task InsertFoodAsync(Food food);
    Task UpdateFoodAsync(Food updatedFood);
    Task DeleteFoodAsync(string? barCode);
    Task<IEnumerable<Food>> ListUnrevisedFoodsAsync();
}