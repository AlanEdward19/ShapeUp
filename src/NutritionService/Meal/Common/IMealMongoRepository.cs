namespace NutritionService.Meal.Common;

public interface IMealMongoRepository
{
    Task<Meal?> GetMealByIdAsync(string? id);
    Task InsertMealAsync(Meal meal);
    Task UpdateMealAsync(Meal updatedMeal, CancellationToken cancellationToken = default);
    Task DeleteMealAsync(string? id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Meal>> GetManyMealsByIdsAsync(string[] itemMealIds, CancellationToken cancellationToken);
    Task<IEnumerable<Meal>> ListMealsAsync(int itemPage, int itemRows, CancellationToken cancellationToken,
        string userId);
}