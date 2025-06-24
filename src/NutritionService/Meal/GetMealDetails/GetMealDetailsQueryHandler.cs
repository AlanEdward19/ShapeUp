using NutritionService.Common.Interfaces;
using NutritionService.Meal.Common;
using SharedKernel.Exceptions;

namespace NutritionService.Meal.GetMealDetails;

/// <summary>
/// Handles the retrieval of meal details by ID.
/// </summary>
/// <param name="repository"></param>
public class GetMealDetailsQueryHandler(IMealMongoRepository repository) : IHandler<MealDto, GetMealDetailsQuery>
{
    /// <summary>
    /// Handles the retrieval of meal details based on the provided query.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<MealDto> HandleAsync(GetMealDetailsQuery item, CancellationToken cancellationToken)
    {
        var meal = await repository.GetMealByIdAsync(item.Id);
        if (meal == null)
            throw new NotFoundException($"Meal with id '{item.Id}' not found");

        return new MealDto(meal);
    }
}