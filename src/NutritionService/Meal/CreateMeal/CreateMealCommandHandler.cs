using NutritionService.Common.Interfaces;
using NutritionService.Meal.Common;

namespace NutritionService.Meal.CreateMeal;

public class CreateMealCommandHandler(IMealMongoRepository repository) : IHandler<Meal, CreateMealCommand>
{
    public async Task<Meal> HandleAsync(CreateMealCommand item, CancellationToken cancellationToken)
    {
        var meal = item.ToMeal();
        
        await repository.InsertMealAsync(meal);

        return meal;
    }
}