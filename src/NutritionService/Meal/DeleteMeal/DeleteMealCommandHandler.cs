using NutritionService.Common.Interfaces;
using NutritionService.Meal.Common;
using SharedKernel.Exceptions;

namespace NutritionService.Meal.DeleteMeal;

public class DeleteMealCommandHandler(IMealMongoRepository repository) : IHandler<Meal, DeleteMealCommand>
{
    public async Task<Meal> HandleAsync(DeleteMealCommand item, CancellationToken cancellationToken)
    {
        var existingMeal = await repository.GetMealByIdAsync(item.Id);
        
        if (existingMeal == null)
            throw new NotFoundException($"Meal with id '{item.Id}' not found");

        await repository.DeleteMealAsync(item.Id);

        return existingMeal;
    }
}