namespace NutritionService.Dish.Common.Repository;

public interface IDishMongoRepository
{
    Task<Dish?> GetDishByIdAsync(string? id);
    Task InsertDishAsync(Dish dish);
    Task UpdateDishAsync(Dish updatedDish);
    Task DeleteDishAsync(string? id);
    
}