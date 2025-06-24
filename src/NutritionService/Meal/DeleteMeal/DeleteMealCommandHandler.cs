using NutritionService.Common.Interfaces;
using NutritionService.Meal.Common;
using SharedKernel.Exceptions;

namespace NutritionService.Meal.DeleteMeal;

/// <summary>
/// Handles the deletion of a meal.
/// </summary>
/// <param name="repository"></param>
public class DeleteMealCommandHandler(IMealMongoRepository repository) : IHandler<bool, DeleteMealCommand>
{
    /// <summary>
    /// Handles the deletion of a meal by its ID.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<bool> HandleAsync(DeleteMealCommand item, CancellationToken cancellationToken)
    {
        var existingMeal = await repository.GetMealByIdAsync(item.Id);
        
        if (existingMeal == null)
            throw new NotFoundException($"Meal with id '{item.Id}' not found");

        await repository.DeleteMealAsync(item.Id);

        return true;
    }
}