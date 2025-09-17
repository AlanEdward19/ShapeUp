namespace NutritionService.Meal.Common;

public interface IMealMongoRepository
{
    Task<Meal?> GetMealByIdAsync(string? id);
    Task InsertMealAsync(Meal meal);
    Task UpdateMealAsync(Meal updatedMeal);
    Task DeleteMealAsync(string? id);
    Task<IEnumerable<Meal>> GetManyMealsByIdsAsync(string[] itemMealIds, CancellationToken cancellationToken);
    Task<IEnumerable<Meal>> ListMealsAsync(int itemPage, int itemRows, CancellationToken cancellationToken,
        string userId);
}