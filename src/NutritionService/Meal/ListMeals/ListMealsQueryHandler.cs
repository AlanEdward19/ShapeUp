using NutritionService.Common.Interfaces;
using NutritionService.Meal.Common;

namespace NutritionService.Meal.ListMeals;

public class ListMealsQueryHandler(IMealMongoRepository repository):
    IHandler<IEnumerable<Meal>, ListMealsQuery>
{
    public async Task<IEnumerable<Meal>> HandleAsync(ListMealsQuery item, CancellationToken cancellationToken)
    {
        return await repository.ListMealsAsync(item.Page, item.Rows, cancellationToken);
    }
}