using NutritionService.Common.Interfaces;
using NutritionService.Meal.Common;
using SharedKernel.Exceptions;

namespace NutritionService.Meal.GetMealDetails;

public class GetMealDetailsQueryHandler(IMealMongoRepository repository) : IHandler<Meal, GetMealDetailsQuery>
{
    public async Task<Meal> HandleAsync(GetMealDetailsQuery item, CancellationToken cancellationToken)
    {
        var meal = await repository.GetMealByIdAsync(item.Id);
        if (meal == null)
            throw new NotFoundException($"Meal with id '{item.Id}' not found");

        return meal;
    }
}