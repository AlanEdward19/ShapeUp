using NutritionService.Common.Interfaces;
using NutritionService.Meal.Common;

namespace NutritionService.Meal.ListMeals;

/// <summary>
/// ListMealsQueryHandler handles the query to list meals.
/// </summary>
/// <param name="repository"></param>
public class ListMealsQueryHandler(IMealMongoRepository repository):
    IHandler<IEnumerable<MealDto>, ListMealsQuery>
{
    /// <summary>
    /// Handles the retrieval of meals based on pagination parameters.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<MealDto>> HandleAsync(ListMealsQuery item, CancellationToken cancellationToken)
    {
        var meals = await repository.ListMealsAsync(item.Page, item.Rows, cancellationToken);
        var mealsDto = meals.Select(meal => new MealDto(meal));
        return mealsDto;
    }
}