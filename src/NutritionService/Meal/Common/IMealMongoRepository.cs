namespace NutritionService.Meal.Common;

public interface IMealMongoRepository
{
    Task<Meal?> GetMealByIdAsync(string? id);
    Task InsertMealAsync(Meal meal);
    Task UpdateMealAsync(Meal updatedMeal);
    Task DeleteMealAsync(string? id);
}